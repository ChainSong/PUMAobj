using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUMAobj.Common
{
    /// <summary>
    /// txt操作类
    /// </summary>
    public static class TextHelper
    {

        /// <summary>
        /// 读取文本文件转换为List 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static List<string> ReadTextFileToList(string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            List<string> list = new List<string>();
            StreamReader sr = new StreamReader(fs, Encoding.UTF8);
            try
            {
                //使用StreamReader类来读取文件 
                sr.BaseStream.Seek(0, SeekOrigin.Begin);
                // 从数据流中读取每一行，直到文件的最后一行
                string tmp = sr.ReadLine();
                while (tmp != null)
                {
                    list.Add(tmp);
                    tmp = sr.ReadLine();
                }
                return list;
            }
            catch (Exception ex)
            {
                return new List<string>();
            }
            finally
            {
                //关闭此StreamReader对象 
                sr.Close();
                fs.Close();
            }
        }

        //将List转换为TXT文件
        public static void WriteListToTextFile(List<string> list, string txtFile)
        {
            FileStream fs = new FileStream(txtFile, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, Encoding.Unicode);
            try
            {
                //创建一个文件流，用以写入或者创建一个StreamWriter 
                sw.Flush();
                // 使用StreamWriter来往文件中写入内容 
                sw.BaseStream.Seek(0, SeekOrigin.Begin);
                for (int i = 0; i < list.Count; i++) sw.WriteLine(list[i]);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //关闭此文件t 
                sw.Flush();
                sw.Close();
                fs.Close();
            }

        }

        /// <summary>
        /// 截取字符串去空格
        /// </summary>
        /// <param name="str"></param>
        /// <param name="start"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string TxtSubstring(this string str, int start, int end)
        {
            return str.Substring(start - 1, end - start + 1).Trim();
        }

        /// <summary>
        /// 右边自动加字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string TxtPadRightstring(this string str, int len, char charstr = ' ')
        {
            return str.PadRight(len, charstr);
        }

        /// <summary>
        /// 左边自动加字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string TxtPadLeftstring(this string str, int len, char charstr = ' ')
        {
            return str.PadLeft(len, charstr);
        }

        /// <summary>
        /// 将字符转换为 时间
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static DateTime? TxtConvertTime(this string str)
        {
            DateTime? time=null;
            try
            {
                time = Convert.ToDateTime(str);
                return time;
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

        /// <summary>
        /// 将字符转换成  数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int TxtConvertInt(this string str) {
            int num = 0;
            try
            {
                num = Convert.ToInt32(str);
                return num;
            }
            catch (Exception)
            {
                return 0;
                throw;
            }
        }

        /// <summary>
        /// 将字符转换成  float
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static float TxtConvertFloat(this string str)
        {
            float num = 0;
            try
            {
                num = Convert.ToSingle(str);
                return num;
            }
            catch (Exception)
            {
                return 0;
                throw;
            }
        }

        
        /// <summary>
        /// 字符超出最大长度要截取 ，字符小于最大长度要补空格
        /// </summary>
        /// <param name="str"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static string TxtStrPush(this string str,int max)
        {
            string S ="";
            if (str.Length >= max)
            {
                S = str.Substring(0, max);
            }
            else if (str.Length < max) {
                S += str.PadRight(max, ' ');
            }
            return S;
        }
    }
}
