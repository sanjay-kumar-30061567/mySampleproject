using LoyalityWebAPI.LoyalityDataTypes;
using LoyalityWebAPI.LoyalityDataTypes.CIDataTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RestSharp;
using RestSharp.Authenticators.Digest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Serilog;
using LoyalityWebAPI.LoyalityDataTypes.CapDataType;
using Microsoft.AspNetCore.Authorization;

namespace LoyalityWebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class LoyalityController : ControllerBase
    {
        private IConfiguration _configuration;
        private string _baseLink = string.Empty;
        private string _aPIUserName = string.Empty;
        private string _aPIPassword = string.Empty;
        private bool _isCISelected = false;

        public LoyalityController(IConfiguration iconfig)
        {
            _configuration = iconfig;
            _isCISelected = Convert.ToBoolean(_configuration.GetSection("MySettings").GetSection("IsCISelected").Value);
            _baseLink = _isCISelected ? _configuration.GetSection("MySettings").GetSection("CILink").Value : _configuration.GetSection("MySettings").GetSection("CapillaryLink").Value;
            _aPIUserName = _isCISelected ? _configuration.GetSection("MySettings").GetSection("CIUserName").Value : _configuration.GetSection("MySettings").GetSection("CapillaryUserName").Value;
            _aPIPassword = _isCISelected ? _configuration.GetSection("MySettings").GetSection("CIPassword").Value : _configuration.GetSection("MySettings").GetSection("CapillaryPassword").Value;
        }

        [HttpGet]
        [Route("GetBalance")]
        public double GetBalance(string mobileNumber)
        {
            double BalancePoint = double.NaN;

            try
            {
                if (_isCISelected)
                {
                    Log.Information("Welcome");
                    string loyalityID = string.Empty;
                    loyalityID = GetLoyalityID(mobileNumber);
                    if (!string.IsNullOrEmpty(loyalityID))
                    {
                        var ciLink = string.Concat(_baseLink, "merchant/customer/rewardbalance/", loyalityID, "/0");
                        LoyalityConnector loyalityConnector = new LoyalityConnector(ciLink, _aPIUserName, _aPIPassword);
                        var response = JsonSerializer.Deserialize<BalancePoint>(loyalityConnector.WebAPICall());
                        BalancePoint = response.data.FirstOrDefault().crbRewardBalance;
                    }

                }
                else
                {
                    var finallink = string.Concat(_baseLink, "v1.1/customer/get?format=json&gap_to_renew_for=0&user_group=true&mobile=", mobileNumber);
                    LoyalityConnector loyalityConnector = new LoyalityConnector(finallink, _aPIUserName, _aPIPassword);
                    var response = JsonSerializer.Deserialize<CapillaryRegisterResponse>(loyalityConnector.WebAPICall(true));
                    if (Convert.ToBoolean(response.response.status.success))
                    {
                        BalancePoint = response.response.customers.customer.FirstOrDefault().loyalty_points;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString() + ex.StackTrace.ToString());
            }
            return BalancePoint;
        }

        [HttpPost]
        [Route("RegisterMember")]
        public RegisterResponse RegisterMember(RegisterData registerData)
        {
            var reposne1 = new RegisterResponse();
            try
            {
                string finalLink = string.Empty;
                #region
                if (_isCISelected)
                {
                    finalLink = string.Concat(_baseLink, "merchant/customer/search/mobile/", registerData.Mobile);
                    LoyalityConnector loyalityConnector = new LoyalityConnector(finalLink, _aPIUserName, _aPIPassword);
                    var response1 = JsonSerializer.Deserialize<CIUserResponse>(loyalityConnector.WebAPICall());
                    if (response1.data.Count == 0)
                    {
                        #endregion
                        var ciLink = string.Concat(_baseLink, "merchant/customer/save");
                        loyalityConnector = new LoyalityConnector(ciLink, _aPIUserName, _aPIPassword);
                        CIRegister cIRegister = new CIRegister();
                        cIRegister.customer = new LoyalityDataTypes.CIDataTypes.Customer();
                        cIRegister.profile = new Profile();
                        cIRegister.customer.cusEmail = registerData.Email;
                        cIRegister.customer.cusMobile = registerData.Mobile;
                        cIRegister.customer.cusFName = registerData.Fname;
                        cIRegister.customer.cusLName = registerData.Lname;
                        cIRegister.customer.cusLoyaltyId = registerData.Mobile;
                        cIRegister.customer.cusStatus = 1;
                        cIRegister.profile.cspCustomerBirthday = "1900-01-01";
                        var body = JsonSerializer.Serialize(cIRegister);
                        CIRegisterResponse response = JsonSerializer.Deserialize<CIRegisterResponse>(loyalityConnector.WebAPICall(body));
                        reposne1.errorcode = response.errorcode;
                        reposne1.status = response.status;
                        reposne1.points = 100;
                    }
                    else
                    {
                        reposne1.errorcode = "user exists";
                        reposne1.status = "Fail";
                        reposne1.points = 0;
                    }
                }
                else
                {
                    finalLink = string.Concat(_baseLink, "v1.1/customer/add?format=json");
                    LoyalityConnector loyalityConnector = new LoyalityConnector(finalLink, _aPIUserName, _aPIPassword);
                    var capRegRequest = new CapillaryRegisterRequest();
                    capRegRequest.customer = new List<LoyalityDataTypes.CapDataType.Customer>();
                    LoyalityDataTypes.CapDataType.Customer cust = new LoyalityDataTypes.CapDataType.Customer()
                    {
                        mobile = registerData.Mobile,
                        email = registerData.Email,
                        firstname = registerData.Fname,
                        lastname = registerData.Lname
                    };
                    capRegRequest.customer.Add(cust);
                    var body = JsonSerializer.Serialize(capRegRequest);
                    var response1 = JsonSerializer.Deserialize<CapillaryRegisterResponse>(loyalityConnector.WebAPICall(body, true));
                    //if (Convert.ToBoolean(response1.response.status.success))
                    //{
                    reposne1.errorcode = response1.response.status.message;
                    reposne1.status = response1.response.status.success;
                    reposne1.points = 100;
                    //}
                    //else
                    //{
                    //    reposne1.errorcode = "user exists";
                    //    reposne1.status = "Fail";
                    //    reposne1.points = 0;
                    //}
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString() + ex.StackTrace.ToString());
            }
            return reposne1;
        }

        [HttpGet]
        [Route("RetriveMember")]
        public RegisterData RetriveMember(string MobileNumber)
        {
            RegisterData MemberDetails = new RegisterData();
            try
            {
                if (_isCISelected)
                {
                    var ciNewLink = string.Concat(_baseLink, "merchant/customer/search/mobile/", MobileNumber);
                    LoyalityConnector loyalityConnector = new LoyalityConnector(ciNewLink, _aPIUserName, _aPIPassword);
                    var response1 = JsonSerializer.Deserialize<CIUserResponse>(loyalityConnector.WebAPICall());
                    if (response1.data.Count > 0)
                    {
                        MemberDetails.Email = response1.data.FirstOrDefault().cusEmail;
                        MemberDetails.Mobile = response1.data.FirstOrDefault().cusMobile;
                        MemberDetails.Fname = response1.data.FirstOrDefault().cusFName;
                        MemberDetails.Lname = response1.data.FirstOrDefault().cusLName;
                    }
                }
                else
                {
                    var finallink = string.Concat(_baseLink, "v1.1/customer/get?format=json&gap_to_renew_for=0&user_group=true&mobile=", MobileNumber);
                    LoyalityConnector loyalityConnector = new LoyalityConnector(finallink, _aPIUserName, _aPIPassword);
                    var response = JsonSerializer.Deserialize<CapillaryRegisterResponse>(loyalityConnector.WebAPICall(true));
                    if (Convert.ToBoolean(response.response.status.success))
                    {
                        MemberDetails.Email = response.response.customers.customer.FirstOrDefault().email;
                        MemberDetails.Mobile = response.response.customers.customer.FirstOrDefault().mobile;
                        MemberDetails.Fname = response.response.customers.customer.FirstOrDefault().firstname;
                        MemberDetails.Lname = response.response.customers.customer.FirstOrDefault().lastname;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString() + ex.StackTrace.ToString());
            }
            return MemberDetails;
        }

        [HttpPost]
        [Route("EarnPoints")]
        public AddSalesResponse AddSales(AddSales addSales)
        {
            AddSalesResponse addSalesResponse = new AddSalesResponse();
            try
            {
                if (_isCISelected)
                {
                    var ciNewLink = string.Concat(_baseLink, "transaction/sale");
                    LoyalityConnector loyalityConnector = new LoyalityConnector(ciNewLink, _aPIUserName, _aPIPassword);
                    CISales cISales = new CISales();
                    cISales.salAmount = addSales.isDutyFree ? addSales.salAmount * 2 : addSales.salAmount;
                    cISales.salLoyaltyId = GetLoyalityID(addSales.mobileNumber);
                    cISales.salDate = DateTime.Now.ToString("yyyy-MM-dd");
                    cISales.salType = 2;
                    cISales.salQuantity = addSales.Quantity;
                    cISales.salPaymentReference = addSales.PaymentReference;
                    cISales.saleSKUSet = new System.Collections.Generic.List<SaleSKUSet>();
                    SaleSKUSet saleSKUSet = new SaleSKUSet();
                    saleSKUSet.ssuProductCode = addSales.ProductCode;
                    saleSKUSet.ssuPrice = addSales.isDutyFree ? addSales.salAmount * 2 : addSales.salAmount;
                    saleSKUSet.ssuQty = addSales.Quantity;
                    cISales.saleSKUSet.Add(saleSKUSet);
                    var body = JsonSerializer.Serialize(cISales);
                    var response = loyalityConnector.WebAPICall(body);
                    if (response.Contains("success"))
                    {
                        addSalesResponse.isSuccess = true;
                        addSalesResponse.PointsEarned = addSales.isDutyFree ? Math.Floor((addSales.salAmount) * 2 / 100) : Math.Floor((addSales.salAmount) / 100);
                    }
                    else
                    {
                        addSalesResponse.isSuccess = true;
                        addSalesResponse.PointsEarned = 0.0m;
                    }
                }
                else
                {
                    //1.First do transaction and take transaction id
                    var finallink = string.Concat(_baseLink, "v2/transactions?source=instore&identifierValue=" + addSales.mobileNumber + "&identifierName=mobile");
                    LoyalityConnector loyalityConnector = new LoyalityConnector(finallink, _aPIUserName, _aPIPassword);

                    LoyalityDataTypes.CapDataType.Transaction_Earning transact = new LoyalityDataTypes.CapDataType.Transaction_Earning()
                    {
                        type = "REGULAR",
                        returnType = "AMOUNT",
                        billAmount = (double)addSales.salAmount,
                        billNumber = addSales.PaymentReference
                    };
                    var body = JsonSerializer.Serialize(transact);
                    var response = JsonSerializer.Deserialize<LoyalityDataTypes.CapDataType.CapillaryTransactionResponse>(loyalityConnector.WebAPICall(body, true));

                    //2.Get Transaction id and then Earn points
                    if (response.createdId > 0)
                    {
                        var finallink1 = string.Concat(_baseLink, "v2/earning/", response.createdId);
                        LoyalityConnector loyalityConnector1 = new LoyalityConnector(finallink1, _aPIUserName, _aPIPassword);
                        var response1 = JsonSerializer.Deserialize<CapillaryRegisterResponse>(loyalityConnector1.WebAPICall(true));
                        //if (Convert.ToBoolean(response1.response.status.success))
                        //{
                        addSalesResponse.isSuccess = true;
                        //}
                    }
                    else
                    {
                        addSalesResponse.isSuccess = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString() + ex.StackTrace.ToString());
            }
            return addSalesResponse;
        }

        [HttpPost]
        [Route("RedeemPoints")]
        public bool RedeemPoints(RedeemPoints redeemPoints)
        {
            bool flag = false;
            try
            {
                if (_isCISelected)
                {
                    var client = new RestClient(string.Concat(_baseLink, "merchant/redemption/cashback"));
                    client.Authenticator = new DigestAuthenticator(_aPIUserName, _aPIPassword);
                    client.Timeout = -1;
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                    request.AddParameter("rwdCurrencyId", "6405");
                    request.AddParameter("loyaltyId", GetLoyalityID(redeemPoints.mobileNumber));
                    request.AddParameter("purchaseUnit", redeemPoints.Points);
                    request.AddParameter("txnref", "Redeem001");
                    IRestResponse response = client.Execute(request);
                    if (response.Content.Contains("success"))
                    {
                        flag = true;
                    }
                }
                else
                {
                    var finallink = string.Concat(_baseLink, "v1.1/points/redeem?program_id=778&format=json&skip_validation=true");
                    LoyalityConnector loyalityConnector = new LoyalityConnector(finallink, _aPIUserName, _aPIPassword);

                    //LoyalityDataTypes.CapDataType.Redeem redeem = new LoyalityDataTypes.CapDataType.Redeem()
                    //{
                    //    points_redeemed = redeemPoints.Points,
                    //    mobile = redeemPoints.mobileNumber
                    //};

                    string strRedeem = "{'root': { 'redeem': [{'points_redeemed': '" + redeemPoints.Points + "', 'customer': {  'mobile': '" + redeemPoints.mobileNumber + "'}} ]}}";

                    //var body = JsonSerializer.Serialize(strRedeem);
                    var response = JsonSerializer.Deserialize<LoyalityDataTypes.CapDataType.CapillaryRedeemResponse>(loyalityConnector.WebAPICall(strRedeem, true));
                    if (response.response.status.success == "true")
                    {
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString() + ex.StackTrace.ToString());
            }
            return flag;
        }

        [HttpPost]
        [Route("TransactionHistory")]
        public List<TransactionResponse> GetTransactionHistory(TransactionRequest transactionRequest)
        {
            List<TransactionResponse> lsttransactions = new List<TransactionResponse>();
            List<transaction> lsttransactions1 = new List<transaction>();
            try
            {
                if (_isCISelected)
                {
                    string endDate = string.Empty;
                    string startDate = string.Empty;
                    if (transactionRequest.StartDate == DateTime.MinValue)
                    {
                        startDate = "1980-01-01";
                    }
                    else
                    {
                        startDate = transactionRequest.StartDate.ToString("yyyy-dd-MM");
                    }
                    if (transactionRequest.EndDate == DateTime.MinValue)
                    {
                        endDate = "9999-12-31";
                    }
                    else
                    {
                        endDate = transactionRequest.EndDate.ToString("yyyy-dd-MM");
                    }

                    var ciLink = string.Concat(_baseLink, "merchant/customer/transactions/", GetLoyalityID(transactionRequest.MobileNumber), "/", startDate, "/", endDate, "?page.page=1&page.size=100");
                    LoyalityConnector loyalityConnector = new LoyalityConnector(ciLink, _aPIUserName, _aPIPassword);
                    var response = JsonSerializer.Deserialize<CITransactions>(loyalityConnector.WebAPICall());
                    foreach (var data in response.data)
                    {
                        var transaction = new TransactionResponse
                        {
                            TXNType = data.txnType,
                            txnMerchantNo = data.txnMerchantNo,
                            txnLoyaltyId = data.txnLoyaltyId,
                            txnStatus = data.txnStatus,
                            txnDate = data.txnDate,
                            Amount = data.txnAmount,
                            PreBalance = data.txnRewardPreBal,
                            PostBalance = data.txnRewardPostBal
                        };
                        lsttransactions.Add(transaction);
                    }
                }
                else
                {
                    var finallink = string.Concat(_baseLink, "v1.1/customer/get?mobile=" + transactionRequest.MobileNumber + "&mlp=true");
                    LoyalityConnector loyalityConnector = new LoyalityConnector(finallink, _aPIUserName, _aPIPassword);
                    //var response = JsonSerializer.Deserialize<CapillaryRegisterResponse>(loyalityConnector.WebAPICall(true));
                    var response = loyalityConnector.WebAPICall(true);

                    System.Xml.XmlDocument xmltest = new System.Xml.XmlDocument();
                    xmltest.LoadXml(response);
                    //System.Xml.XmlNodeList elemlist = xmltest.GetElementsByTagName("transactions");
                    //string result = elemlist[0].InnerXml;
                    //System.Xml.XmlNode node = xmltest.DocumentElement.SelectSingleNode("/transactions");
                    System.Xml.XmlNodeList data = xmltest.GetElementsByTagName("transactions");
                    System.Xml.XmlNodeList elemList1 = xmltest.GetElementsByTagName("transactions");
                    for (int i = 0; i < elemList1.Count; i++)
                    {
                        var transaction = new transaction
                        {
                            id = Convert.ToInt32(elemList1[i].Attributes["id"].Value),
                            number = Convert.ToInt32(elemList1[i].Attributes["number"].Value),
                            type = elemList1[i].Attributes["type"].Value,
                            created_date = elemList1[i].Attributes["created_date"].Value,
                            store = elemList1[i].Attributes["store"].Value
                        };
                        lsttransactions1.Add(transaction);
                    }

                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString() + ex.StackTrace.ToString());
            }
            return lsttransactions;
        }

        private string GetLoyalityID(string MobileNumber)
        {
            string loyalityID = string.Empty;
            try
            {
                var ciNewLink = string.Concat(_baseLink, "merchant/customer/search/mobile/", MobileNumber);
                LoyalityConnector loyalityConnector = new LoyalityConnector(ciNewLink, _aPIUserName, _aPIPassword);
                var response1 = JsonSerializer.Deserialize<CIUserResponse>(loyalityConnector.WebAPICall());
                if (response1.data.Count > 0)
                {
                    loyalityID = response1.data.FirstOrDefault().cusLoyaltyId;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString() + ex.StackTrace.ToString());
            }
            return loyalityID;
        }
    }
}
