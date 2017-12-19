using System;

namespace F4HApp
{
    class MemberModel
    {
    }

    public class LoginResponseObject
    {
        public string MemberId { get; set; }
        public string Name { get; set; }
        public string IsLoggedIn { get; set; }
        public string Msg { get; set; }
        public string LastLoginDate { get; set; }
    }

    public class RegistrationResponseObject
    {
        public string Status { get; set; }
        public string Msg { get; set; }
    }
    public class SMSResponceObject
    {
        public string Status { get; set; }
    }

    public class MemberPointsResponseObject
    {
        public string Status { get; set; }
        public int Earned { get; set; }
        public int Redeemed { get; set; }
        public int Transfered { get; set; }
        public string Msg { get; set; }
    }

    public class MemberDetailResponseObject
    {
        public string Status { get; set; }
        public string MobileNo { get; set; }
        public string EmailID { get; set; }
        public string AddLine1 { get; set; }
        public string AddLine2 { get; set; }
        public string AddLine3 { get; set; }
        public string CityTown { get; set; }
        public string StateArea { get; set; }
        public string PostCode { get; set; }
        public string Country { get; set; }
    }

    public class AboutUsResponceObject
    {
        public string AboutUsMessage { get; set; }
    }

    public class AboutPatnerResponceObject
    {
        public string AboutPatnerMessage { get; set; }
    }
    public class AboutHelathPlanResponceObject
    {
        public string AboutHelathPlanMessage { get; set; }
    }

}
