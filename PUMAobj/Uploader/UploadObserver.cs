using PUMAobj.SqlHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUMAobj.Uploader
{
    /// <summary>
    /// 建立(上传观察者)模型基类
    /// </summary>
    public abstract class UploadObserver : BaseAccessor
    {
        protected UploadObserver(ModelBase childModel)
        {
            //childModel.SubEvent += new ModelBase.SubEventHandler(UploadReceipt);
            //childModel.SubEvent += new ModelBase.SubEventHandler(UploadOrder);

            childModel.SubEvent += new ModelBase.SubEventHandler(Put);
            //childModel.SubEvent += new ModelBase.SubEventHandler(UploadReceiptOrderCancel);
            //childModel.SubEvent += new ModelBase.SubEventHandler(UploadInventoryStatusChange);
            //childModel.SubEvent += new ModelBase.SubEventHandler(UploadInventoryQtyChange);
            //childModel.SubEvent += new ModelBase.SubEventHandler(UploadExpressDelivery);
            //childModel.SubEvent += new ModelBase.SubEventHandler(UploadInventorySnapshot);
            //childModel.SubEvent += new ModelBase.SubEventHandler(UploadInventorySnapshot_MinorIngredientInventoryAdjust);
            //childModel.SubEvent += new ModelBase.SubEventHandler(UploadInventorySnapshot_MinorIngredient);
        }

        //上传入库单
        //public abstract bool UploadReceipt();
        //上传出库单
        //public abstract bool UploadOrder();
        ////上传销售出库单
        public abstract bool Put();
        ////上传订单取消
        //public abstract bool UploadReceiptOrderCancel();
        ////上传库存状态修改 
        //public abstract bool UploadInventoryStatusChange();
        ////上传库存数量修改
        //public abstract bool UploadInventoryQtyChange();
        ////上传快递交接
        //public abstract bool UploadExpressDelivery();
        ////上传库存快照
        //public abstract bool UploadInventorySnapshot();

        //public abstract bool UploadInventorySnapshot_MinorIngredient();
        //public abstract bool UploadInventorySnapshot_MinorIngredientInventoryAdjust();



    }
}
