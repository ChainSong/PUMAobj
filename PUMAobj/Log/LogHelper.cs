using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
[assembly: log4net.Config.XmlConfigurator(ConfigFileExtension = "config", Watch = true)]


namespace PUMAobj.Log
{
    public   class LogHelper
    {
        public enum LogLevel { FATAL = 1, Error = 2, Warn = 3, INFO = 4 }

        /// <summary>
        /// 输出日志到Log4Net
        /// </summary>
        /// <param name="t"></param>
        /// <param name="ex"></param>
        public static void WriteLog(Type t, Exception ex)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Error("Error", ex);
        }


        /// <summary>
        /// 输出日志到Log4Net
        /// </summary>
        /// <param name="t"></param>
        /// <param name="msg"></param>
        public static void WriteLog(Type t, string msg, LogLevel level)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            switch (level.ToString())
            {
                case "FATAL":
                    log.Fatal(msg);
                    break;
                case "Error":
                    log.Error(msg);
                    break;
                case "Warn":
                    log.Warn(msg);
                    break;
                case "INFO":
                    log.Info(msg);
                    break;
                default:
                    break;
            }
        }
    }
}
