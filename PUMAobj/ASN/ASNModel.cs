using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUMAobj.ASN
{
    public class ASNModel
    {
        public string HeaderFlag { get; set; }
        public string InterfaceActionFlag { get; set; }
        public string POKey { get; set; }
        public string ExternPOKey { get; set; }
        public string PoGroup { get; set; }
        public string StorerKey { get; set; }
        public string PODate { get; set; }
        public string SellersReference { get; set; }
        public string BuyersReference { get; set; }
        public string OtherReference { get; set; }
        public string POType { get; set; }
        public string SellerName { get; set; }
        public string SellerAddress1 { get; set; }
        public string SellerAddress2 { get; set; }
        public string SellerAddress3 { get; set; }
        public string SellerAddress4 { get; set; }
        public string SellerCity { get; set; }
        public string SellerState { get; set; }
        public string SellerZip { get; set; }
        public string SellerPhone { get; set; } 
    }
}
