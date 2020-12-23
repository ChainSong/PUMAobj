using PUMAobj.SqlHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUMAobj.Downloader
{
    /// <summary>
    /// 建立(下载观察者)模型基类
    /// </summary>
    public abstract class DownloadObserver : BaseAccessor
    {
        protected DownloadObserver(ModelBase childModel)
        {
            //childModel.SubEvent += new ModelBase.SubEventHandler(GetBaoZunSKU);
            childModel.SubEvent += new ModelBase.SubEventHandler(Get);

        }
        //下载商品信息
        //public abstract bool GetBaoZunSKU();
        ////下载销售出库单
        public abstract bool Get();

    }
}
