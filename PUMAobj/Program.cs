using PUMAobj.ASN;
using PUMAobj.Downloader;
using PUMAobj.Log;
using PUMAobj.MessageContracts;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace PUMAobj
{
   public class Program
    {
        static void Main(string[] args)
        {

            var defparams =
               (Dictionary<string, DefConfigurationSectionRequest>)ConfigurationManager.GetSection("RunningParam");
            //RunCLS.RunAbutment(defparams);

            DownloadInterface downloadInterface = new DownloadInterface();

            Download download = new Download(downloadInterface, defparams,
                                             DateTime.Now.ToString("yyyyMMddHHmmss") + "Test");
            LogHelper.WriteLog(typeof(string), "--开始下载接口对接--", LogHelper.LogLevel.INFO);
            downloadInterface.StartDownload();
            LogHelper.WriteLog(typeof(string), "--结束下载接口对接--", LogHelper.LogLevel.INFO);


            //DownloadInterface uploadInterface = new DownloadInterface();


            ASNAccessor aSN = new ASNAccessor();
            //aSN.GetInbound_ASNHD();
            //Upload upload = new Upload(uploadInterface, defparams,
            //                           DateTime.Now.ToString("yyyyMMddHHmmss") + "Test");

            //LogHelper.WriteLog(typeof(string), "--开始上传接口对接--", LogHelper.LogLevel.INFO);
            //uploadInterface.StartDownload();
            //LogHelper.WriteLog(typeof(string), "--结束上传接口对接--", LogHelper.LogLevel.INFO);

            //ASNAccessor aSN = new ASNAccessor();
            //aSN.GetInbound_ASNHD();

            string msg = "";
            //string txtaddress = aSN.Create_RECHD_TXT1(null, null, out msg);
            //string txtaddress = aSN.Create_SHPTXT(null, null, out msg);

            //aSN.wms_receipt();
            //aSN.Create_SHPPK();
            //aSN.WMSAdjustment();
            //aSN.WMSInventory();

            //aSN.ExternTable("3400344248");

            //string txtaddress = aSN.GetInbound_ASNHD(null, out msg);


            //aSN.CreatIQC("210104002");
        }
    }
}
