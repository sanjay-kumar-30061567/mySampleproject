using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoyalityWebAPI.LoyalityDataTypes.CIDataTypes
{
    public class CIRegisterResponse
    {
        public string errorcode { get; set; }
        public string status { get; set; }
        public int data { get; set; }
    }
}
