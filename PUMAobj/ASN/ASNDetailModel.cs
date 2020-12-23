using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUMAobj.ASN
{
    public class ASNDetailModel
    {
        public string HeaderFlag { get; set; }
        public string InterfaceActionFlag { get; set; }
        public string POKey { get; set; }
        public string POLineNumber { get; set; }
        public string StorerKey { get; set; }
        public string PODetailKey { get; set; }
        public string ExternPOKey { get; set; }
        public string ExternLineNo { get; set; }
        public string MarksContainer { get; set; }
        public string Sku { get; set; }
        public string SKUDescription { get; set; }
        public string ManufacturerSku { get; set; }
        public string RetailSku { get; set; }
        public string AltSku { get; set; }
        public string QtyOrdered { get; set; }
        public string QtyAdjusted { get; set; }
        public string QtyReceived { get; set; }
        public string PackKey { get; set; }
        public string UnitPrice { get; set; }
        public string UOM { get; set; }
    }
}
