using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoyalityWebAPI.LoyalityDataTypes.CIDataTypes
{
   
    public class Datum
    {
        public int txnId { get; set; }
        public int txnType { get; set; }
        public int txnMerchantNo { get; set; }
        public string txnLoyaltyId { get; set; }
        public int txnStatus { get; set; }
        public string txnDate { get; set; }
        public int txnLocation { get; set; }
        public string txnInternalRef { get; set; }
        public string txnExternalRef { get; set; }
        public int txnRewardCurrencyId { get; set; }
        public int txnCrDbInd { get; set; }
        public double txnRewardQty { get; set; }
        public double txnAmount { get; set; }
        public int txnProgramId { get; set; }
        public double txnRewardPreBal { get; set; }
        public double txnRewardPostBal { get; set; }
        public string txnRewardExpDt { get; set; }
        public List<object> saleSKUResources { get; set; }
        public string rwdCurrencyName { get; set; }
        public object createdAt { get; set; }
        public List<object> links { get; set; }
    }

    public class CITransactions
    {
        public string totalelements { get; set; }
        public List<Datum> data { get; set; }
        public string totalpages { get; set; }
        public string pagenumber { get; set; }
        public string status { get; set; }
    }



}
