using PUMAobj.ASN;
using PUMAobj.Log;
using PUMAobj.MessageContracts;
using PUMAobj.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUMAobj.Uploader
{
    /// <summary>
    /// 上传观察者
    /// </summary>
    public class Upload : UploadObserver
    {
        //日期参数
        private Dictionary<string, DefConfigurationSectionRequest> _paramList;
        public Dictionary<string, DefConfigurationSectionRequest> paramList { get { return _paramList; } }

        //批次号
        private string batchNumber;
        public string BatchNumber
        {
            get { return batchNumber; }
            set { batchNumber = value; }
        }

        public Upload(ModelBase childModel, Dictionary<string, DefConfigurationSectionRequest> param, string batchNumber)
            : base(childModel)
        {
            _paramList = param;
            this.batchNumber = batchNumber;
        }

        public override bool Put()
        {
            try
            {
                //获取配置参数
                DefConfigurationSectionRequest DefRequest =
                     this._paramList.SingleOrDefault(
                        o => o.Key == "SKURequest").Value;
                Request request = null;
                if (DefRequest != null)
                {
                    LogHelper.WriteLog(typeof(string), batchNumber + ":<<上传", LogHelper.LogLevel.INFO);
                    switch (DefRequest.runningMode)
                    {
                        case "off":
                            LogHelper.WriteLog(typeof(string), batchNumber + ":上传的模式不存在", LogHelper.LogLevel.Warn);
                            break;
                        case "on_once": //执行单次模式，按照配置表提供的参数执行

                            //请求参数
                            request = new Request(DefRequest.startTime, DefRequest.endTime, DefRequest.page,
                                                                   DefRequest.pageSize);
                            //return (GetSKU(batchNumber, request));
                            break;
                        case "on_continuity": ///执行连续模式，获取起始日期和终止日期的时间差，用于配置
                            ASNAccessor Accessor = new ASNAccessor();
                            Accessor.wms_receipt();//上传入库反馈
                            Accessor.Create_SHPPK();//上传出库反馈
                            Accessor.WMSAdjustment();//上传调整反馈
                            //Accessor.WMSInventory();//上传库存快照反馈
                            return true;
                        default:
                            LogHelper.WriteLog(typeof(string), batchNumber + ":上传的模式不存在", LogHelper.LogLevel.Error);
                            break;
                    }
                    return true;
                }
                else
                {
                    LogHelper.WriteLog(typeof(string), batchNumber + ":请求参数为空，无法继续",
                                       LogHelper.LogLevel.Error);
                    return false;
                }
            }
            catch
                (Exception
                    ex)
            {
                LogHelper.WriteLog(ex.GetType(), batchNumber + ":" + ex.Message, LogHelper.LogLevel.Error);
                return false;
            }
            finally
            {
                LogHelper.WriteLog(typeof(string), batchNumber + ">>结束上传", LogHelper.LogLevel.INFO);
            }
        }

    }
}
