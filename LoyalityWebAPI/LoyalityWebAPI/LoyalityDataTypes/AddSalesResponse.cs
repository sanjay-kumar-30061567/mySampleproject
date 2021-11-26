using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoyalityWebAPI.LoyalityDataTypes
{
    public class AddSalesResponse
    {
        public bool isSuccess { get; set; }
        public decimal PointsEarned { get; set; }
    }
}
