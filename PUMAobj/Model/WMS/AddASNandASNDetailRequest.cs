using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUMAobj.Model.WMS
{
   public class AddASNandASNDetailRequest
    {
        public IEnumerable<ASNH> asn { get; set; }
        public IEnumerable<ASNDetail> asnDetails { get; set; }
    }
}
