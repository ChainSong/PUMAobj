using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUMAobj.ASN
{
    public class InboundASNDTModel
    {
        public string HeaderFlag { get; set; }
        public string InterfaceActionFlag { get; set; }
        public string ReceiptKey { get; set; }
        public string ReceiptLineNumber { get; set; }
        public string ExternReceiptKey { get; set; }
        public string ExternLineNo { get; set; }
        public string StorerKey { get; set; }
        public string POKey { get; set; }
        public string Sku { get; set; }
        public string AltSku { get; set; }
        public string Id { get; set; }
        public string Status { get; set; }
        public string DateReceived { get; set; }
        public string QtyExpected { get; set; }
        public string QtyAdjusted { get; set; }
        public string QtyReceived { get; set; }
        public string UOM { get; set; }
        public string PackKey { get; set; }
        public string VesselKey { get; set; }
        public string VoyageKey { get; set; }
    }
}
