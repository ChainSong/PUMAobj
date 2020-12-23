using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUMAobj.ASN
{
    public class InboundASNHDModel
    {
        
        public string HeaderFlag { get; set; }
        public string InterfaceActionFlag { get; set; }
        public string ReceiptKey { get; set; }
        public string ExternReceiptKey { get; set; }
        public string ReceiptGroup { get; set; }
        public string StorerKey { get; set; }
        public string ReceiptDate { get; set; }
        public string POKey { get; set; }
        public string CarrierKey { get; set; }
        public string CarrierName { get; set; }
        public string CarrierAddress1 { get; set; }
        public string CarrierAddress2 { get; set; }
        public string CarrierCity { get; set; }
        public string CarrierState { get; set; }
        public string CarrierZip { get; set; }
        public string CarrierReference { get; set; }
        public string WarehouseReference { get; set; }
        public string OriginCountry { get; set; }
        public string DestinationCountry { get; set; }
        public string VehicleNumber { get; set; }
    }
}
