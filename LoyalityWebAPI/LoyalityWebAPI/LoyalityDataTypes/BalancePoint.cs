using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoyalityWebAPI.LoyalityDataTypes
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Datum
    {
        public string crbLoyaltyId { get; set; }
        public int crbMerchantNo { get; set; }
        public int crbRewardCurrency { get; set; }
        public double crbRewardBalance { get; set; }
        public string rwdCurrencyName { get; set; }
        public double rwdCashbackValue { get; set; }
        public double rwdRatioDeno { get; set; }
        public bool drawChance { get; set; }
        public List<object> links { get; set; }
    }
    public class BalancePoint
    {
        public List<Datum> data { get; set; }
        public string status { get; set; }
    }

}
