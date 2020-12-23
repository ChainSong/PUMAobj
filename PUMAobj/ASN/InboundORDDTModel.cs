using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUMAobj.ASN
{
    public class InboundORDDTModel
    {
        public string HeaderFlag { get; set; }
        public string InterfaceActionFlag { get; set; }
        public string OrderLineNumber { get; set; }
        public string OrderDetailSysId { get; set; }
        public string ExternOrderKey { get; set; }
        public string ExternLineNo { get; set; }
        public string Sku { get; set; }
        public string StorerKey { get; set; }
        public string ManufacturerSku { get; set; }
        public string RetailSku { get; set; }
        public string AltSku { get; set; }
        public string OriginalQty { get; set; }
        public string OpenQty { get; set; }
        public string ShippedQty { get; set; }
        public string AdjustedQty { get; set; }
        public string QtyPreAllocated { get; set; }
        public string QtyAllocated { get; set; }
        public string QtyPicked { get; set; }
        public string UOM { get; set; }
    }
}
