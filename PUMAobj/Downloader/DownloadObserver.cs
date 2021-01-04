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
            childModel.SubEvent += new ModelBase.SubEventHandler(Business);

        }
        //下载商品信息
        //public abstract bool GetBaoZunSKU();
        /// <summary>
        /// 单据下载
        /// </summary>
        /// <returns></returns>
        public abstract bool Get();
        /// <summary>
        /// 业务处理
        /// </summary>
        /// <returns></returns>
        public abstract bool Business();

    }
}
