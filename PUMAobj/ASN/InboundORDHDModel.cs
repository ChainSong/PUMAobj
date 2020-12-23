using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUMAobj.ASN
{
    public class InboundORDHDModel
    {
        public string HeaderFlag { get; set; }
        public string InterfaceActionFlag { get; set; }
        public string OrderKey { get; set; }
        public string StorerKey { get; set; }
        public string ExternOrderKey { get; set; }
        public string Reserved { get; set; }
        public string OrderDate { get; set; }
        public string DeliveryDate { get; set; }
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
    }
}
