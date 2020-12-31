using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUMAobj.Common
{
    [AttributeUsage(AttributeTargets.Property)]
    public class EntityPropertyExtensionAttribute : System.Attribute
    {
        public EntityPropertyExtensionAttribute(string dbTableColumnName, string viewTableColumnName)
        {
            DBTableColumnName = dbTableColumnName;
            ViewTableColumnName = viewTableColumnName;
        }

        public EntityPropertyExtensionAttribute(string dbTableColumnName, string viewTableColumnName, string toStringVal)
        {
            DBTableColumnName = dbTableColumnName;
            ViewTableColumnName = viewTableColumnName;
            ToStringVal = toStringVal;
        }

        public string DBTableColumnName { get; set; }

        public string ToStringVal { get; set; }

        public string ViewTableColumnName { get; set; }
    }
}
