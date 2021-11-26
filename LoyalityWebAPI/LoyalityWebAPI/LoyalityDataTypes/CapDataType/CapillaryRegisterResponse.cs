using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoyalityWebAPI.LoyalityDataTypes.CapDataType
{

    public class Status
    {
        public string success { get; set; }
        public int code { get; set; }
        public string message { get; set; }
        public string total { get; set; }
        public string success_count { get; set; }
    }

    public class PointsSummary
    {
        public string programId { get; set; }
        public string redeemed { get; set; }
        public string expired { get; set; }
        public string returned { get; set; }
        public string adjusted { get; set; }
        public string lifetimePoints { get; set; }
        public string loyaltyPoints { get; set; }
        public string cumulativePurchases { get; set; }
        public string currentSlab { get; set; }
        public string nextSlab { get; set; }
        public string nextSlabSerialNumber { get; set; }
        public string nextSlabDescription { get; set; }
        public string slabSNo { get; set; }
        public string slabExpiryDate { get; set; }
        public string totalPoints { get; set; }
    }

    public class PointsSummaries
    {
        public List<PointsSummary> points_summary { get; set; }
    }



    public class Subscriptions
    {
        public List<object> subscription { get; set; }
    }

    public class SideEffects
    {
        public List<object> effect { get; set; }
    }

    public class Warnings
    {
        public List<object> warning { get; set; }
    }

    public class ItemStatus
    {
        public string success { get; set; }
        public string code { get; set; }
        public string message { get; set; }
        public Warnings warnings { get; set; }
    }

    public class Customers
    {
        public List<Customer> customer { get; set; }
    }

    public class Response
    {
        public Status status { get; set; }
        public Customers customers { get; set; }
    }

    public class CapillaryRegisterResponse
    {
        public Response response { get; set; }
    }


    public class CapillaryTransactionResponse
    {
        public Int32 createdId { get; set; }
        //public string warnings { get; set; }
        //public string errors { get; set; }
        //public Customers customerInfo { get; set; }
    }


    public class CapillaryRedeemResponse
    {
        public Response response { get; set; }
    }
    public class RedeemResponse
    {
        public Status status { get; set; }
    }
    public class CapillaryTransactionResponse1
    {
        public TransactionBodyResponse response { get; set; }
    }

    public class TransactionBodyResponse
    {
        public Status status { get; set; }
        public List<Customers1> customers { get; set; }
    }

    //public class TransactionBodyCustomers
    //{
    //    public List<Customers1> customer { get; set; }
    //}

    public class Customers1
    {
        public string firstname { get; set; }
        public int lastname { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public Int32 lifetime_points { get; set; }

        public Int32 lifetime_purchases { get; set; }
        public int loyalty_points { get; set; }
        public string registered_by { get; set; }
        public List<transaction> transactions { get; set; }

    }

    public class transaction
    {
        public Int32 id { get; set; }
        public Int32 number { get; set; }
        public string type { get; set; }
        public string created_date { get; set; }
        public string store { get; set; }
    }
}