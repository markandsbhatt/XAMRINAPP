using System;
using System.Collections.Generic;

namespace F4HApp
{
    class TransactionModel
    {
    }

    public class EarnResponseObject
    {
        public string Status { get; set; }
        public int Points { get; set; }        
        public string Msg { get; set; }
    }

    public class EarnPointListResponseObject
    {
        public string ID { get; set; }
        public string Date { get; set; }
        public string ScratchCode { get; set; }
        public string Mode { get; set; }
        public string Points { get; set; }
    }

    public class RedeemPointListResponseObject
    {
        public string ID { get; set; }
        public string Date { get; set; }
        public string Points { get; set; }
        public string Purchase { get; set; }
    }

    public class TransferPointListResponseObject
    {
        public string ID { get; set; }
        public string Date { get; set; }
        public string Points { get; set; }
        public string TransferedTo { get; set; }
    }

    public class AllPointListResponseObject
    {
        public string ID { get; set; }
        public string Date { get; set; }
        public string Points { get; set; }
        public string Mode { get; set; }
        public string Remarks { get; set; }

    }

    public class EarnPointList
    {
        public List<EarnPointListResponseObject> contacts { get; set; }
    }
}
