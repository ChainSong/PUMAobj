using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUMAobj
{


    /// <summary>
    /// 建立下载目标类
    /// </summary>
    public class DownloadInterface : ModelBase
    {
        public void StartDownload()
        {
            this.StartAbutment(); //开始对接
        }
    }
}
