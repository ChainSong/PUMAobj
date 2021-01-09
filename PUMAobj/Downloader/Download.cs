using PUMAobj.ASN;
using PUMAobj.Common;
using PUMAobj.Log;
using PUMAobj.MessageContracts;
using PUMAobj.Product;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUMAobj.Downloader
{
    public class Download : DownloadObserver
    {
        private Dictionary<string, DefConfigurationSectionRequest> _paramList;

        //批次号
        private string batchNumber;
        public string BatchNumber
        {
            get { return batchNumber; }
            set { batchNumber = value; }
        }

        public Download(ModelBase childModel, Dictionary<string, DefConfigurationSectionRequest> param, string batchNumber)
            : base(childModel)
        {
            _paramList = param;
            this.batchNumber = batchNumber;
        }
        //public override bool Get()
        //{
        //    try
        //    {
        //        //获取配置参数
        //        DefConfigurationSectionRequest DefRequest =
        //             this._paramList.SingleOrDefault(
        //                o => o.Key == "SKURequest").Value;
        //        Request request = null;
        //        if (DefRequest != null)
        //        {
        //            LogHelper.WriteLog(typeof(string), batchNumber + ":<<读取商品", LogHelper.LogLevel.INFO);
        //            switch (DefRequest.runningMode)
        //            {
        //                case "off":
        //                    LogHelper.WriteLog(typeof(string), batchNumber + ":读取商品接口为关闭模式", LogHelper.LogLevel.Warn);
        //                    break;
        //                case "on_once": //执行单次模式，按照配置表提供的参数执行

        //                    //请求参数
        //                    request = new Request(DefRequest.startTime, DefRequest.endTime, DefRequest.page,
        //                                                           DefRequest.pageSize);
        //                    //return (GetSKU(batchNumber, request));
        //                    break;
        //                case "on_continuity": ///执行连续模式，获取起始日期和终止日期的时间差，用于配置
        //                    //计算配置参数中起始日期和终止日期的秒差
        //                    DateTime startTime = Convert.ToDateTime(DefRequest.startTime);
        //                    DateTime endTime = Convert.ToDateTime(DefRequest.endTime);
        //                    TimeSpan ts = endTime.Subtract(startTime);
        //                    bool istrue = true;
        //                    for (int i = 1; i < DefRequest.pageMax; i++)
        //                    {

        //                        ///请求参数
        //                        request = new Request(DateTime.Now.AddSeconds(-ts.TotalSeconds).ToString(
        //                                                        "yyyy-MM-dd HH:mm:ss"),
        //                                                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),

        //                                                    DefRequest.page,
        //                                                    DefRequest.pageSize);

        //                        //istrue = (GetSKU(batchNumber, request));
        //                        //DefRequest.page = i;
        //                        break;
        //                    };
        //                    return istrue;
        //                default:
        //                    LogHelper.WriteLog(typeof(string), batchNumber + ":读取商品接口不存在的模式", LogHelper.LogLevel.Error);
        //                    break;
        //            }
        //            return true;
        //        }
        //        else
        //        {
        //            LogHelper.WriteLog(typeof(string), batchNumber + ":请求参数为空，无法继续",
        //                               LogHelper.LogLevel.Error);
        //            return false;
        //        }
        //    }
        //    catch
        //        (Exception
        //            ex)
        //    {
        //        LogHelper.WriteLog(ex.GetType(), batchNumber + ":" + ex.Message, LogHelper.LogLevel.Error);
        //        return false;
        //    }
        //    finally
        //    {
        //        LogHelper.WriteLog(typeof(string), batchNumber + ">>结束读取商品", LogHelper.LogLevel.INFO);
        //    }
        //}

        public override bool Get()
        {
            //return true;
            try
            {
                //连接FTP 将文件下载到本地
                //FtpHelper ftpHelper = new FtpHelper("","","");
                //先获取文件中的列表
                //开始上传SFTP/
                string SFTPIP = SFTPConstants.sftpip;
                //ConfigurationManager.AppSettings["SFTPIP"];
                string SFTPort = SFTPConstants.sftpport;
                //= ConfigurationManager.AppSettings["SFTPort"];
                string SFTPUser = SFTPConstants.sftpuser;
                //ConfigurationManager.AppSettings["SFTPUser"];
                string SFTPPwd = SFTPConstants.sftppwd;
                //ConfigurationManager.AppSettings["SFTPPwd"];
                string SFTPPath = SFTPConstants.sftpfilepath;
                string sftpfilepath_successful = SFTPConstants.sftpfilepath_successful;


                string ReceiveFilePath = SFTPConstants.ReceiveFilePath;
                //ConfigurationManager.AppSettings["SFTPPath"];

                SFTPHelper fTPHelper = new SFTPHelper(SFTPIP, SFTPort, SFTPUser, SFTPPwd);
                var fileNames = fTPHelper.GetFileList(SFTPPath, "txt");
                foreach (var item in fileNames)
                {
                    bool results = fTPHelper.Get(SFTPPath, ReceiveFilePath);
                    if (results)
                    {
                        fTPHelper.Move(SFTPPath, sftpfilepath_successful);
                    }
                }
            }
            catch (Exception)
            {

                //throw;
            }
            return true;
        }

        public override bool Business()
        {
            try
            {
                //读取文件列表
                string[] receivefiles = Directory.GetFiles(SFTPConstants.ReceiveFilePath);
                if (receivefiles.Length > 0)
                {
                    //TextHelper txthelper = new TextHelper();
                    for (int i = 0; i < receivefiles.Length; i++)
                    {
                        LogModel log = new LogModel();
                        log.SourceFileName = receivefiles[i];

                        FileInfo file = new FileInfo(receivefiles[i]);
                        string filename = file.Name;
                        string result = "";//解析的错误提示
                        string externumber = "";
                        try
                        {
                            List<string> txtlists = TextHelper.ReadTextFileToList(receivefiles[i]);//读取成list

                            //没有数据
                            if (txtlists.Count() > 0)
                            {
                                //可以处理多个接口文件
                                switch (txtlists[0].ToString().Substring(0, 9).Trim())
                                {
                                    case "WMSSKU"://PUMA推给我们的
                                        log.Type = "WMSSKU";
                                        result = new ProductAccessor().AddProduct(txtlists, out externumber);
                                        break;
                                    case "WMSASN"://PUMA推给我们的 入库单
                                        log.Type = "WMSASN";
                                        result = new ASNAccessor().GetInbound_ASNHD(txtlists, out externumber);
                                        break;
                                    case "WMSORD"://PUMA推给我们的出库单
                                        log.Type = "WMSORD";
                                        result = new ASNAccessor().GetInbound_ASNHD(txtlists, out externumber);
                                        break;
                                    default:
                                        log.Type = "";
                                        result = "未能从文件中识别出对应的接口";
                                        break;
                                }

                                if (result == "")
                                {
                                    //解析成功，移动到success文件夹
                                    log.ToFileName = SFTPConstants.SuccessFilePath + @"\" + log.Type + @"\" + filename;
                                    log.ResultDesc = "解析成功";
                                    log.Externumber = externumber;
                                    log.Flag = "Y";
                                }

                                else
                                {
                                    if (log.Type != "")
                                    {
                                        if (result.Contains("数据库插入失败"))
                                        {
                                            log.ToFileName = "";// SFTPConstants.SuccessFilePath + @"\" + log.Type + @"\" + filename;
                                            log.ResultDesc = "解析失败：" + result;
                                            log.Externumber = externumber;
                                            log.Flag = "E";
                                        }
                                        else
                                        {
                                            log.ToFileName = SFTPConstants.FaildFilePath + @"\" + log.Type + @"\" + filename;//移动到解析失败文件夹                                            
                                            log.ResultDesc = "解析失败：" + result;
                                            log.Externumber = externumber;
                                            log.Flag = "N";
                                        }
                                    }
                                    else
                                    {
                                        log.ToFileName = SFTPConstants.ErrorFilePath + @"\" + filename;
                                        log.ResultDesc = "解析失败：" + result;
                                        log.Externumber = externumber;
                                        log.Flag = "N";
                                    }
                                }
                            }
                            else
                            {
                                log.ToFileName = SFTPConstants.ErrorFilePath + @"\" + filename;
                                log.Flag = "N";
                                log.ResultDesc = "解析失败：文档中无数据";
                            }

                        }
                        catch (Exception ex)
                        {
                            //报错了放到error文件
                            log.ToFileName = SFTPConstants.ErrorFilePath + @"\" + filename;
                            log.Flag = "N";
                            log.ResultDesc = "解析报错：" + ex.Message.ToString();
                        }
                        if (log.Flag == "E")//数据库失败再解析一次
                        {

                        }
                        else
                        {
                            //FileCommon.MoveToCover(log.SourceFileName, log.ToFileName);
                        }
                    }
                }
            }
            catch (Exception)
            {
                //throw;
            }
            return true;
        }
    }
}
