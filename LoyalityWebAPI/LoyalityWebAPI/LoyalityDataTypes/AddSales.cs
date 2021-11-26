using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoyalityWebAPI.LoyalityDataTypes
{


    public class AddSales
    {
        public string mobileNumber { get; set; }      
        public decimal salAmount { get; set; }
        public int Quantity { get; set; }
        public string PaymentReference { get; set; }
        public string ProductCode { get; set; }
        public bool isDutyFree { get; set; }

    }

}
