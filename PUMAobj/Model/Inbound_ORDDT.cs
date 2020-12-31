using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUMAobj.Model
{
   public class Inbound_ORDDT
    {
        //ASN 详细信息
        public long ID { get; set; }
        public long Pid { get; set; }

        public string HeaderFlag { get; set; }
        public string InterfaceActionFlag { get; set; }
        public string OrderLineNumber { get; set; }
        public int OrderDetailSysId { get; set; }
        public string ExternOrderKey { get; set; }
        public string ExternLineNo { get; set; }
        public string Sku { get; set; }
        public string StorerKey { get; set; }
        public string ManufacturerSku { get; set; }
        public string RetailSku { get; set; }
        public string AltSku { get; set; }
        public int OriginalQty { get; set; }
        public int OpenQty { get; set; }
        public int ShippedQty { get; set; }
        public int AdjustedQty { get; set; }
        public int QtyPreAllocated { get; set; }
        public int QtyAllocated { get; set; }
        public int QtyPicked { get; set; }
        public string UOM { get; set; }
        public string PackKey { get; set; }
        public string PickCode { get; set; }
        public string CartonGroup { get; set; }
        public string Lot { get; set; }
        public string TID { get; set; }
        public string Facility { get; set; }
        public string Status { get; set; }
        public float UnitPrice { get; set; }
        public float Tax01 { get; set; }
        public float Tax02 { get; set; }
        public float ExtendedPrice { get; set; }
        public string Reserved { get; set; }
        public string UpdateSource { get; set; }
        public string Lottable01 { get; set; }
        public string Lottable02 { get; set; }
        public string Lottable03 { get; set; }
        public DateTime? Lottable04 { get; set; }
        public DateTime? Lottable05 { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public string TariffKey { get; set; }
        public int FreeGoodQty { get; set; }
        public float GrossWeight { get; set; }
        public string WgtUnit { get; set; }
        public float Capacity { get; set; }
        public string CubeUnit { get; set; }
        public string LoadKey { get; set; }
        public string MBOLKey { get; set; }
        public int QtyToProcess { get; set; }
        public int MinShelfLife { get; set; }
        public string UserDefine01 { get; set; }
        public string UserDefine02 { get; set; }
        public string UserDefine03 { get; set; }
        public string UserDefine04 { get; set; }
        public string UserDefine05 { get; set; }
        public string UserDefine06 { get; set; }
        public string UserDefine07 { get; set; }
        public string UserDefine08 { get; set; }
        public string UserDefine09 { get; set; }
        public string POkey { get; set; }
        public string ExternPOKey { get; set; }
        public string OrgExternLineNo { get; set; }

        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
