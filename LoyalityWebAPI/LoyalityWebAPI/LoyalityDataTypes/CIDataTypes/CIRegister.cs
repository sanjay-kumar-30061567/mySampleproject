using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoyalityWebAPI.LoyalityDataTypes.CIDataTypes
{    
    public class Customer
    {
        public string cusLoyaltyId { get; set; }
        public string cusEmail { get; set; }
        public string cusMobile { get; set; }
        public string cusFName { get; set; }
        public string cusLName { get; set; }
        public int cusStatus { get; set; }
    }

    public class Profile
    {
        public string cspAddress { get; set; }
        public string cspCity { get; set; }
        public string cspStateDesc { get; set; }
        public string cspAddRef1 { get; set; }
        public string cspPincode { get; set; }
        public string cspGender { get; set; }
        public string cspAddRef2 { get; set; }
        public string cspCustomerBirthday { get; set; }
    }

    public class CIRegister
    {
        public Customer customer { get; set; }
        public Profile profile { get; set; }
    }




}
