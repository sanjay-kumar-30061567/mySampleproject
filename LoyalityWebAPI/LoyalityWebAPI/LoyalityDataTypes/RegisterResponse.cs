using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoyalityWebAPI.LoyalityDataTypes
{
    public class RegisterResponse
    {
        public string errorcode { get; set; }
        public string status { get; set; }
        public int points { get; set; }
    }
}
