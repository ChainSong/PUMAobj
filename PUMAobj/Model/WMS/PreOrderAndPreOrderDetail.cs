using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUMAobj.Model.WMS
{
   public class PreOrderAndPreOrderDetail
    {

        public IEnumerable<PreOrderDetail> PreOd { get; set; }

        public PreOrder PreO { get; set; }

        public IEnumerable<PreOrder> PreOrderList { get; set; }

        public IEnumerable<PreOrderDetail> PreOrderDetailList { get; set; }
    }
}
