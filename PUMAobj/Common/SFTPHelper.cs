﻿using PUMAobj.Log;
using Renci.SshNet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUMAobj.Common
{
    public class SFTPHelper
    {
        #region 字段或属性
        private SftpClient sftp;
        /// <summary>
        /// SFTP连接状态
        /// </summary>
        public bool Connected { get { return sftp.IsConnected; } }
        #endregion


        #region 构造
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="ip">IP</param>
        /// <param name="port">端口</param>
        /// <param name="user">用户名</param>
        /// <param name="pwd">密码</param>
        public SFTPHelper(string ip, string port, string user, string pwd)
        {
            sftp = new SftpClient(ip, Int32.Parse(port), user, pwd);

            Connect();
        }

        ~SFTPHelper()
        {
            Disconnect();
        }
        #endregion

        #region 连接SFTP
        /// <summary>
        /// 连接SFTP
        /// </summary>
        /// <returns>true成功</returns>
        public bool Connect()
        {
            try
            {
                if (!Connected)
                {
                    sftp.Connect();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("连接SFTP失败，原因：{0}", ex.Message));
            }
        }
        #endregion

        #region 断开SFTP
        /// <summary>
        /// 断开SFTP
        /// </summary> 
        public void Disconnect()
        {
            try
            {
                if (sftp != null && Connected)
                {
                    sftp.Disconnect();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("断开SFTP失败，原因：{0}", ex.Message));
            }
        }
        #endregion

        #region SFTP上传文件
        /// <summary>
        /// SFTP上传文件
        /// </summary>
        /// <param name="localPath">本地路径</param>
        /// <param name="remotePath">远程路径</param>
        public bool Put(string localPath, string remotePath, string SFTPPath)
        {
            try
            {
                using (var file = File.OpenRead(localPath))
                {
                    Connect();
                    //sftp.ChangeDirectory(@"\NIKEReturn\Receive");

                    sftp.ChangeDirectory(SFTPPath); //先注释看看

                    sftp.UploadFile(file, remotePath);
                    Disconnect();
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(string), localPath + ":SFTP文件上传失败 原因：:" + ex.ToString(), LogHelper.LogLevel.Error);
                //throw new Exception(string.Format("SFTP文件上传失败，原因：{0}", ex.Message));
            }
            return false;
        }
        #endregion

        public bool PUMAPut(string localPath, string localfilename, string remotePath)
        {
            try
            {
                using (var file = File.OpenRead(localPath + "/" + localfilename))
                {
                    Connect();
                    //sftp.ChangeDirectory(@"\NIKEReturn\Receive");
                    sftp.ChangeDirectory(remotePath); //先注释看看
                    sftp.UploadFile(file, localfilename);
                    Disconnect();
                }
                LocalFileHelper.MoveToCover(localPath + localfilename, localPath + "/Success" + localfilename);
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(string), localPath + ":SFTP文件上传失败 原因：:" + ex.ToString(), LogHelper.LogLevel.Error);
                //throw new Exception(string.Format("SFTP文件上传失败，原因：{0}", ex.Message));
            }
            LocalFileHelper.MoveToCover(localPath + localfilename, localPath + "/Warning" + localfilename);
            return false;
        }


        #region SFTP获取文件
        /// <summary>
        /// SFTP获取文件
        /// </summary>
        /// <param name="remotePath">远程路径</param>
        /// <param name="localPath">本地路径</param>
        public bool Get(string remotePath, string localPath)
        {
            try
            {
                Connect();
                var byt = sftp.ReadAllBytes(remotePath);
                Disconnect();
                File.WriteAllBytes(localPath, byt);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                //throw new Exception(string.Format("SFTP文件获取失败，原因：{0}", ex.Message));
            }

        }
        #endregion

        #region 删除SFTP文件
        /// <summary>
        /// 删除SFTP文件 
        /// </summary>
        /// <param name="remoteFile">远程路径</param>
        public void Delete(string remoteFile)
        {
            try
            {
                Connect();
                sftp.Delete(remoteFile);
                Disconnect();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("SFTP文件删除失败，原因：{0}", ex.Message));
            }
        }
        #endregion

        #region 获取SFTP文件列表
        /// <summary>
        /// 获取SFTP文件列表
        /// </summary>
        /// <param name="remotePath">远程目录</param>
        /// <param name="fileSuffix">文件后缀</param>
        /// <returns></returns>
        public ArrayList GetFileList(string remotePath, string fileSuffix)
        {
            try
            {
                Connect();
                var files = sftp.ListDirectory(remotePath);
                Disconnect();
                var objList = new ArrayList();
                foreach (var file in files)
                {
                    if (file.Name.Contains(fileSuffix))
                    {
                        string name = file.Name;
                        objList.Add(name);
                    }
                }
                return objList;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("SFTP文件列表获取失败，原因：{0}", ex.Message));
            }
        }
        #endregion

        #region 移动SFTP文件
        /// <summary>
        /// 移动SFTP文件
        /// </summary>
        /// <param name="oldRemotePath">旧远程路径</param>
        /// <param name="newRemotePath">新远程路径</param>
        public void Move(string oldRemotePath, string newRemotePath)
        {
            try
            {
                Connect();
                sftp.RenameFile(oldRemotePath, newRemotePath);
                Disconnect();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("SFTP文件移动失败，原因：{0}", ex.Message));
            }
        }
        #endregion

    }
}
