using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUMAobj.Model
{
   public class Inbound_ASNDT
    {
        //ASN 详细信息
        public long ID { get; set; }
        public long Pid { get; set; }
        

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
        public DateTime? DateReceived { get; set; }
        public int QtyExpected { get; set; }
        public int QtyAdjusted { get; set; }
        public int QtyReceived { get; set; }
        public string UOM { get; set; }
        public string PackKey { get; set; }
        public string VesselKey { get; set; }
        public string VoyageKey { get; set; }
        public string XdockKey { get; set; }
        public string ContainerKey { get; set; }
        public string ToLoc { get; set; }
        public string ToLot { get; set; }
        public string ToId { get; set; }
        public string ConditionCode { get; set; }
        public string Lottable01 { get; set; }
        public string Lottable02 { get; set; }
        public string Lottable03 { get; set; }
        public DateTime? Lottable04 { get; set; }
        public DateTime? Lottable05 { get; set; }
        public int CaseCnt { get; set; }
        public int InnerPack { get; set; }
        public int Pallet { get; set; }
        public float Cube { get; set; }
        public float GrossWgt { get; set; }
        public float NetWgt { get; set; }
        public float OtherUnit1 { get; set; }
        public float OtherUnit2 { get; set; }
        public float UnitPrice { get; set; }
        public float ExtendedPrice { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public string TariffKey { get; set; }
        public int FreeGoodQtyExpected { get; set; }
        public int FreeGoodQtyReceived { get; set; }
        public string SubReasonCode { get; set; }
        public string FinalizeFlag { get; set; }
        public string DuplicateFrom { get; set; }
        public int BeforeReceivedQty { get; set; }
        public string PutawayLoc { get; set; }
        public string ExportStatus { get; set; }
        public string SplitPalletFlag { get; set; }
        public string POLineNumber { get; set; }
        public string LoadKey { get; set; }
        public string ExternPoKey { get; set; }
        public string UserDefine01 { get; set; }
        public string UserDefine02 { get; set; }
        public string UserDefine03 { get; set; }
        public string UserDefine04 { get; set; }
        public string UserDefine05 { get; set; }
        public DateTime? UserDefine06 { get; set; }
        public DateTime? UserDefine07 { get; set; }
        public string UserDefine08 { get; set; }
        public string UserDefine09 { get; set; }
        public string UserDefine10 { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
