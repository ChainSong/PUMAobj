using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUMAobj.SqlHelper
{
    [Serializable]
    public class DataAccessLayerException : Exception
    {
        public DataAccessLayerException(string message)
            : base(message)
        {
        }

        public DataAccessLayerException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
