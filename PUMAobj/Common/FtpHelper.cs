using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using PUMAobj.Log;
using System.Configuration;

namespace PUMAobj.Common
{
    public class FtpHelper
    {
        private static string FTPCONSTR = "";//FTP的服务器地址
        private static string FTPUSERNAME = "";//FTP服务器的用户名
        private static string FTPPASSWORD = "";//FTP服务器的密码
        private static FtpWebRequest objReqFtp = null; //FTP連接

        public FtpHelper()
        {
            FTPCONSTR = ConfigurationManager.AppSettings["sftpip"].ToString();
            FTPUSERNAME = ConfigurationManager.AppSettings["sftpuser"].ToString();
            FTPPASSWORD = ConfigurationManager.AppSettings["sftppwd"].ToString();
        }

        #region FTP连接
        /// <summary>
        /// 连接ftp方法
        /// </summary>
        /// <param name="path"></param>
        private static void Connect(String path)
        {
            // 根据uri创建FtpWebRequest对象
            objReqFtp = (FtpWebRequest)FtpWebRequest.Create(new Uri(path));
            // 指定数据传输类型
            //To transmit text data, change the UseBinary property from its default value ( true) to false.
            objReqFtp.UseBinary = true;
            // specifies that an SSL connection
            objReqFtp.EnableSsl = false;
            // ftp用户名和密码
            objReqFtp.Credentials = new NetworkCredential(FTPUSERNAME, FTPPASSWORD);
        }
        #endregion

        #region 获取编码方式
        /// <summary>
        /// 获取编码方式
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private static Encoding GetEncodingEncode(WebResponse response)
        {
            Encoding encodingTemp = Encoding.Default;
            StreamReader reader = new StreamReader(response.GetResponseStream());
            encodingTemp = reader.CurrentEncoding;

            if (encodingTemp == Encoding.UTF8)
            {
                encodingTemp = Encoding.UTF8;
            }
            else if (encodingTemp == Encoding.Default)
            {
                encodingTemp = Encoding.GetEncoding("GB2312");
            }
            else
            {
                encodingTemp = Encoding.UTF8;
            }
            return encodingTemp;
        }
        #endregion

        #region 获取FTP目录下所有文件名
        /// <summary>
        /// 读取文件目录下所有的文件名称，包括文件夹名称
        /// </summary>
        /// <param name="ftpAdd">传过来的文件夹路径</param>
        /// <returns>返回的文件或文件夹名称</returns>
        public static string[] GetFtpFileList(string FTPURL)
        {
            string url = FTPCONSTR + FTPURL;

            Connect(url);

            if (objReqFtp != null)
            {
                StringBuilder fileListBuilder = new StringBuilder();
                //ftpRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;//该方法可以得到文件名称的详细资源，包括修改时间、类型等这些属性
                objReqFtp.Method = WebRequestMethods.Ftp.ListDirectory;//只得到文件或文件夹的名称
                try
                {
                    WebResponse ftpResponse = objReqFtp.GetResponse();
                    Encoding encod = GetEncodingEncode(ftpResponse);//获取编码
                    StreamReader ftpFileListReader = new StreamReader(ftpResponse.GetResponseStream(), encod);

                    string line = ftpFileListReader.ReadLine();
                    while (line != null)
                    {
                        fileListBuilder.Append(line);
                        fileListBuilder.Append("@");//每个文件名称之间用@符号隔开，便于前端调用的时候解析
                        line = ftpFileListReader.ReadLine();
                    }
                    ftpFileListReader.Close();
                    ftpResponse.Close();
                    fileListBuilder.Remove(fileListBuilder.ToString().LastIndexOf("@"), 1);
                    return fileListBuilder.ToString().Split('@');//返回得到的数组
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(typeof(string), "获取所有文件名出错:" + ex.Message, LogHelper.LogLevel.Error);
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 文件自动上传
        public static bool UploadFile(string ftpPath, string path, string id)
        {
            FileInfo f = new FileInfo(path);
            path = path.Replace("\\", "/");
            path = FTPCONSTR + ftpPath + id;
            Connect(path);
            objReqFtp.KeepAlive = false;
            objReqFtp.Method = WebRequestMethods.Ftp.UploadFile;
            objReqFtp.ContentLength = f.Length;
            int buffLength = 2048;
            byte[] buff = new byte[buffLength];
            int contentLen;
            FileStream fs = f.OpenRead();
            try
            {
                Stream strm = objReqFtp.GetRequestStream();
                contentLen = fs.Read(buff, 0, buffLength);
                while (contentLen != 0)
                {
                    strm.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                }
                strm.Close();
                fs.Close();
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(string), $"因{ex.Message},无法完成上传:SO3", LogHelper.LogLevel.Error);
                return false;
            }
        }
        #endregion

        #region 从ftp服务器下载文件
        /// <summary>
        /// 从ftp服务器下载文件的功能
        /// </summary>
        /// <param name="ftpfilepath">ftp下载的地址</param>
        /// <param name="filePath">存放到本地的路径</param>
        /// <param name="fileName">保存的文件名称</param>
        /// <returns></returns>
        public static bool Download(string ftpfilepath, string filePath, string fileName)
        {
            try
            {
                String onlyFileName = Path.GetFileName(fileName);
                string newFileName = filePath + onlyFileName;
                if (File.Exists(newFileName))
                {
                    File.Delete(newFileName);//若存在，先刪除
                }
                ftpfilepath = ftpfilepath.Replace("\\", "/");
                string url = FTPCONSTR + ftpfilepath;

                Connect(url);

                FtpWebResponse response = (FtpWebResponse)objReqFtp.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                long cl = response.ContentLength;
                int bufferSize = 2048;
                int readCount;
                byte[] buffer = new byte[bufferSize];
                readCount = ftpStream.Read(buffer, 0, bufferSize);
                FileStream outputStream = new FileStream(newFileName, FileMode.Create);
                while (readCount > 0)
                {
                    outputStream.Write(buffer, 0, readCount);
                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                }
                ftpStream.Close();
                outputStream.Close();
                response.Close();
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(string), $"FTP下载异常{ex.Message}:SO3", LogHelper.LogLevel.Error);
                return false;
            }
        }
        #endregion

        #region 文件移动
        /// <summary>
        /// 包括ftp服务器内移动or重命名,目标目录若已有同名文件，原文件会被删除
        /// </summary>
        /// <param name="currentFilename">当前目录</param> 
        /// <param name="newFilename">新目录</param>********
        public static bool RenameAndMove(string currentFilename, string newFilename)
        {
            try
            {
                //currentFilename = currentFilename.Replace("\\", "/"); //*******完整的文件路徑，包括Ftp服务器地址和文件名和后缀******                      
                string uri = FTPCONSTR + currentFilename;
                Connect(uri);

                objReqFtp.Method = WebRequestMethods.Ftp.Rename;
                objReqFtp.RenameTo = newFilename;//********************新的路径，不需要ftp服务器地址
                objReqFtp.UseBinary = true;
                FtpWebResponse response = (FtpWebResponse)objReqFtp.GetResponse();
                Stream ftpStream = response.GetResponseStream();

                if (ftpStream != null)
                {
                    ftpStream.Close();
                }
                bool success = response.StatusCode == FtpStatusCode.CommandOK || response.StatusCode == FtpStatusCode.FileActionOK;
                response.Close();
                objReqFtp = null;
                return success;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(string), $"文件移动时产生异常{ex.Message}", LogHelper.LogLevel.Error);
                return false;
            }
        }
        #endregion

        #region 从ftp服务器删除文件的功能
        /// <summary>
        /// 从ftp服务器删除文件的功能
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool DeleteFile(string fileName)
        {
            try
            {
                string url = FTPCONSTR + fileName;
                Connect(url);
                objReqFtp.Method = WebRequestMethods.Ftp.DeleteFile;

                FtpWebResponse response = (FtpWebResponse)objReqFtp.GetResponse();
                response.Close();
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(string), $"文件刪除時產生異常{ex.Message}", LogHelper.LogLevel.Error);
                return false;
            }
        }
        #endregion

    }
}
