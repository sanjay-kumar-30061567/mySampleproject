using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoyalityWebAPI.LoyalityDataTypes.CIDataTypes
{   
    public class SaleSKUSet
    {
        public string ssuProductCode { get; set; }
        public int ssuQty { get; set; }
        public decimal ssuPrice { get; set; }
    }

    public class CISales
    {
        public string salLoyaltyId { get; set; }
        public string salDate { get; set; }
        public int salType { get; set; }
        public decimal salAmount { get; set; }
        public int salQuantity { get; set; }
        public string salPaymentReference { get; set; }
        public List<SaleSKUSet> saleSKUSet { get; set; }
    }



}
