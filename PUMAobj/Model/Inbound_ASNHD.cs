using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUMAobj.Model
{
   public class Inbound_ASNHD
    {
        //ASN 主订单信息
        public long ID { get; set; }

        public string HeaderFlag { get; set; }
        public string InterfaceActionFlag { get; set; }
        public string ReceiptKey { get; set; }
        public string ExternReceiptKey { get; set; }
        public string ReceiptGroup { get; set; }
        public string StorerKey { get; set; }
        public DateTime? ReceiptDate { get; set; }
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
        public string VehicleDate { get; set; }
        public string PlaceOfLoading { get; set; }
        public string PlaceOfDischarge { get; set; }
        public string PlaceofDelivery { get; set; }
        public string IncoTerms { get; set; }
        public string TermsNote { get; set; }
        public string ContainerKey { get; set; }
        public string Signatory { get; set; }
        public string PlaceofIssue { get; set; }
        public int OpenQty { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
        public string EffectiveDate { get; set; }
        public string ContainerType { get; set; }
        public int ContainerQty { get; set; }
        public int BilledContainerQty { get; set; }
        public string RECType { get; set; }
        public string ASNStatus { get; set; }
        public string ASNReason { get; set; }
        public string Facility { get; set; }
        public string Reserved { get; set; }
        public string MBOLKey { get; set; }
        public string Appointment_No { get; set; }
        public string LoadKey { get; set; }
        public string xDockFlag { get; set; }
        public string UserDefine01 { get; set; }
        public string PROCESSTYPE { get; set; }
        public string UserDefine02 { get; set; }
        public string UserDefine03 { get; set; }
        public string UserDefine04 { get; set; }
        public string UserDefine05 { get; set; }
        public DateTime? UserDefine06 { get; set; }
        public DateTime? UserDefine07 { get; set; }
        public string UserDefine08 { get; set; }
        public string UserDefine09 { get; set; }
        public string UserDefine10 { get; set; }
        public string DOCTYPE { get; set; }
        public string RoutingTool { get; set; }
        public string CTNTYPE1 { get; set; }
        public int CTNQTY1 { get; set; }
        public string CTNTYPE2 { get; set; }
        public int CTNQTY2 { get; set; }
        public string CTNTYPE3 { get; set; }
        public int CTNQTY3 { get; set; }
        public string CTNTYPE4 { get; set; }
        public int CTNQTY4 { get; set; }
        public string CTNTYPE5 { get; set; }
        public int CTNQTY5 { get; set; }
        public string CTNTYPE6 { get; set; }
        public int CTNQTY6 { get; set; }
        public string CTNTYPE7 { get; set; }
        public int CTNQTY7 { get; set; }
        public string CTNTYPE8 { get; set; }
        public int CTNQTY8 { get; set; }
        public string CTNTYPE9 { get; set; }
        public int CTNQTY9 { get; set; }
        public string CTNTYPE10 { get; set; }
        public int CTNQTY10 { get; set; }
        public int NoOfBulkCtn { get; set; }
        public int NoOfMiniPacks { get; set; }
        public int NoOfPallet { get; set; }
        public float Weight { get; set; }
        public string WeightUnit { get; set; }
        public float Cube { get; set; }
        public string CubeUnit { get; set; }

        public int ISReturn { get; set; }
        public DateTime? ReturnDate { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

    }
}
