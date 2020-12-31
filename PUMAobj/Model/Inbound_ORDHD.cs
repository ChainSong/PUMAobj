using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUMAobj.Model
{
   public class Inbound_ORDHD
    {
        //ASN 工厂 主订单信息
        public long ID { get; set; }

        public string HeaderFlag { get; set; }
        public string InterfaceActionFlag { get; set; }
        public string OrderKey { get; set; }
        public string StorerKey { get; set; }
        public string ExternOrderKey { get; set; }
        public string Reserved { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string Priority { get; set; }
        public string ConsigneeKey { get; set; }
        public string C_contact1 { get; set; }
        public string C_Contact2 { get; set; }
        public string C_Company { get; set; }
        public string C_Address1 { get; set; }
        public string C_Address2 { get; set; }
        public string C_Address3 { get; set; }
        public string C_Address4 { get; set; }
        public string C_City { get; set; }
        public string C_State { get; set; }
        public string C_Zip { get; set; }
        public string C_Country { get; set; }
        public string C_ISOCntryCode { get; set; }
        public string C_Phone1 { get; set; }
        public string C_Phone2 { get; set; }
        public string C_Fax1 { get; set; }
        public string C_Fax2 { get; set; }
        public string C_vat { get; set; }
        public string BuyerPO { get; set; }
        public string BillToKey { get; set; }
        public string B_contact1 { get; set; }
        public string B_Contact2 { get; set; }
        public string B_Company { get; set; }
        public string B_Address1 { get; set; }
        public string B_Address2 { get; set; }
        public string B_Address3 { get; set; }
        public string B_Address4 { get; set; }
        public string B_City { get; set; }
        public string B_State { get; set; }
        public string B_Zip { get; set; }
        public string B_Country { get; set; }
        public string B_ISOCntryCode { get; set; }
        public string B_Phone1 { get; set; }
        public string B_Phone2 { get; set; }
        public string B_Fax1 { get; set; }
        public string B_Fax2 { get; set; }
        public string B_Vat { get; set; }
        public string IncoTerm { get; set; }
        public string PmtTerm { get; set; }
        public int OpenQty { get; set; }
        public string Status { get; set; }
        public string DischargePlace { get; set; }
        public string DeliveryPlace { get; set; }
        public string IntermodalVehicle { get; set; }
        public string CountryOfOrigin { get; set; }
        public string CountryDestination { get; set; }
        public string UpdateSource { get; set; }
        public string Type { get; set; }
        public string OrderGroup { get; set; }
        public string Door { get; set; }
        public string Route { get; set; }
        public string Stop { get; set; }
        public string Notes { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public string ContainerType { get; set; }
        public int ContainerQty { get; set; }
        public int BilledContainerQty { get; set; }
        public string SOStatus { get; set; }
        public string MBOLKey { get; set; }
        public string InvoiceNo { get; set; }
        public float InvoiceAmount { get; set; }
        public string Salesman { get; set; }
        public float GrossWeight { get; set; }
        public string WgtUnit { get; set; }
        public float Capacity { get; set; }
        public string CubeUnit { get; set; }
        public string PrintFlag { get; set; }
        public string LoadKey { get; set; }
        public string Rdd { get; set; }
        public string Notes2 { get; set; }
        public int SequenceNo { get; set; }
        public string Rds { get; set; }
        public string SectionKey { get; set; }
        public string Facility { get; set; }
        public DateTime? PrintDocDate { get; set; }
        public string LabelPrice { get; set; }
        public string POKey { get; set; }
        public string ExternPOKey { get; set; }
        public string XDockFlag { get; set; }
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
        public string Issued { get; set; }
        public string DeliveryNote { get; set; }
        public DateTime? PODCust { get; set; }
        public DateTime? PODArrive { get; set; }
        public DateTime? PODReject { get; set; }
        public string PODUser { get; set; }
        public string xdockpokey { get; set; }
        public string SpecialHandling { get; set; }
        public string RoutingTool { get; set; }
        public string MarkforKey { get; set; }
        public string M_Contact1 { get; set; }
        public string M_Contact2 { get; set; }
        public string M_Company { get; set; }
        public string M_Address1 { get; set; }
        public string M_Address2 { get; set; }
        public string M_Address3 { get; set; }
        public string M_Address4 { get; set; }
        public string M_City { get; set; }
        public string M_State { get; set; }
        public string M_Zip { get; set; }
        public string M_Country { get; set; }
        public string M_ISOCntryCode { get; set; }
        public string M_Phone1 { get; set; }
        public string M_Phone2 { get; set; }
        public string M_Fax1 { get; set; }
        public string M_Fax2 { get; set; }
        public string M_vat { get; set; }
        public string C_State_Long { get; set; }

        public int ISReturn { get; set; }
        public DateTime? ReturnDate { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
