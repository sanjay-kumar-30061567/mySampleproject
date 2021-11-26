using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoyalityWebAPI.LoyalityDataTypes
{
    public class TransactionResponse
    {
        public int TXNType { get; set; }
        public int txnMerchantNo { get; set; }
        public string txnLoyaltyId { get; set; }
        public int txnStatus { get; set; }
        public string txnDate { get; set; }
        public double Amount { get; set; }
        public double PreBalance { get; set; }
        public double PostBalance { get; set; }


    }
}
