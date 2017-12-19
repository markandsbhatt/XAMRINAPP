using System;

namespace F4HApp
{
    class MasterModel
    {
    }

    public class CountryResponseObject
    {        
        public string CountryName { get; set; }
    }

    public class NotificationtResponseObject
    {
        public string Notification { get; set; }
        public string NotificationDate { get; set; }
    }

    public class SupportResponseObject
    {
        public string Notification { get; set; }
        public string Answer { get; set; }
        public string NotificationDate { get; set; }
    }

    public class MemberFFResponseObject
    {
        public string Name { get; set; }
        public string FFCategory { get; set; }
        public string MobileNo { get; set; }
        public string IsMember { get; set; }
        public string FFMemberID { get; set; }
        public bool F4HMember { get; set; }
        public bool ButtonShowHide { get; set; }
    }

    public class RedeemResponseObject
    {
        public string SchemeDetail { get; set; }
        public string EPointID { get; set; }
        public int RedeemPoints { get; set; }
    }
}
