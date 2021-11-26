using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoyalityWebAPI.LoyalityDataTypes.CapDataType
{

    public class Field
    {
        public string name { get; set; }
        public string value { get; set; }
    }

    public class CustomFields
    {
        public List<Field> field { get; set; }
    }

    public class ExtendedFields
    {
        public List<Field> field { get; set; }
    }

    public class Customer
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string external_id { get; set; }
        public int lifetime_points { get; set; }
        public int lifetime_purchases { get; set; }
        public int loyalty_points { get; set; }
        public string current_slab { get; set; }
        public string registered_on { get; set; }
        public string updated_on { get; set; }
        public string type { get; set; }
        public string source { get; set; }
        public List<object> identifiers { get; set; }
        public object gender { get; set; }
        public string registered_by { get; set; }
        public RegisteredStore registered_store { get; set; }
        public RegisteredTill registered_till { get; set; }
        public List<object> user_groups2 { get; set; }
        public FraudDetails fraud_details { get; set; }
        public string trackers { get; set; }
        public object current_nps_status { get; set; }
        public CustomFields custom_fields { get; set; }
        public ExtendedFields extended_fields { get; set; }
        public Transactions transactions { get; set; }
        public Coupons coupons { get; set; }
        public List<object> notes { get; set; }
        public PointsSummaries points_summaries { get; set; }
        public ItemStatus item_status { get; set; }
    }

    public class Transaction
    {
        public string id { get; set; }
        public string number { get; set; }
        public string type { get; set; }
        public string created_date { get; set; }
        public string store { get; set; }

        public double billAmount { get; set; }
        public string billNumber { get; set; }
        public string identifierValue { get; set; }
        public string returnType { get; set; }
        public string identifierName { get; set; }
        public string source { get; set; }
    }

    public class Transaction_Earning
    {
        public string type { get; set; }
        public string returnType { get; set; }
        public double billAmount { get; set; }
        public string billNumber { get; set; }

    }

    public class Transactions
    {
        public List<Transaction> transaction { get; set; }
    }

    public class Coupons
    {
        public List<object> coupon { get; set; }
    }

    public class RegisteredStore
    {
        public string code { get; set; }
        public string name { get; set; }
    }

    public class RegisteredTill
    {
        public string code { get; set; }
        public string name { get; set; }
    }

    public class FraudDetails
    {
        public string status { get; set; }
        public string marked_by { get; set; }
        public string modified_on { get; set; }
        public string reason { get; set; }
    }

    public class CapillaryRegisterRequest
    {
        public List<Customer> customer { get; set; }
    }

    public class Redeem
    {
        public Int32 points_redeemed { get; set; }
        public string mobile { get; set; }

    }
}
