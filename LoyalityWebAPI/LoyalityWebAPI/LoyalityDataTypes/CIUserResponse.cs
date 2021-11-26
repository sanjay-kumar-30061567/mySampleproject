using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoyalityWebAPI.LoyalityDataTypes
{
    public class CIUserResponse
    {
        public string totalelements { get; set; }
        public List<Datum> data { get; set; }
        public string totalpages { get; set; }
        public string pagenumber { get; set; }
        public string status { get; set; }
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public class CusAttrs
        {
            public string APPLICATION_STATUS { get; set; }
            public string TITLE_EMAIL_UPDATE_REQUIRED { get; set; }
            public string SOURCE { get; set; }
            public string PROFILE_UPDATED_AT { get; set; }
            public DateTime APPLICATION_DECISION_DATE { get; set; }
            public string PRODUCT_VARIANT { get; set; }
            public string REMARKS { get; set; }
            public string ARN { get; set; }
            public DateTime APPLICATION_CREATION_DATE { get; set; }
        }

        public class Datum
        {
            public int cusCustomerNo { get; set; }
            public int cusMerchantNo { get; set; }
            public string cusLoyaltyId { get; set; }
            public string cusEmail { get; set; }
            public string cusMobile { get; set; }
            public string cusFName { get; set; }
            public string cusLName { get; set; }
            public string cusIdNo { get; set; }
            public int cusStatus { get; set; }
            public int cusMerchantUserRegistered { get; set; }
            public int cusLocation { get; set; }
            public int cusType { get; set; }
            public int cusTier { get; set; }
            public string cspCustomerBirthday { get; set; }
            public int cspProfession { get; set; }
            public int cspIncomeRange { get; set; }
            public int cspAgeGroup { get; set; }
            public string cspGender { get; set; }
            public int cspFamilyStatus { get; set; }
            public string cspAddress { get; set; }
            public string cspCity { get; set; }
            public string cspPincode { get; set; }
            public int cspState { get; set; }
            public int cspCountry { get; set; }
            public string cspFamilyChild1Name { get; set; }
            public string cspFamilyChild2Name { get; set; }
            public string cspFamilySpouseName { get; set; }
            public int cspPreferredStaff1 { get; set; }
            public int cspCustomerSegmentId1 { get; set; }
            public int cspIsPrivilegedMember { get; set; }
            public string cspNomineeName { get; set; }
            public string cspNomineeLName { get; set; }
            public string cspNomineeRelation { get; set; }
            public string cspNomineeAddress { get; set; }
            public string cspNomineeLoyaltyId { get; set; }
            public string tieName { get; set; }
            public string cusMobileAlternate { get; set; }
            public string cspAddRef1 { get; set; }
            public string cspAddRef2 { get; set; }
            public string cspAddRef3 { get; set; }
            public string cspAddRef4 { get; set; }
            public string cspAddRef5 { get; set; }
            public string referralCode { get; set; }
            public int cusDebitLocked { get; set; }
            public int cusNomineeEnabled { get; set; }
            public List<object> customerNomineeSet { get; set; }
            public CusAttrs cusAttrs { get; set; }
            public List<object> partnerCustomerProfileResourceSet { get; set; }
            public string cusExternalId { get; set; }
            public string cspStateDesc { get; set; }
            public string cusRegisterTimestamp { get; set; }
            public bool primary { get; set; }
            public List<object> links { get; set; }
        }

        public class Root
        {
          
        }


    }
}
