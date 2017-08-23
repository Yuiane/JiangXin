using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace JiangXin.Common
{
    /// <summary>
    /// 日志帮助类
    /// </summary>
    public class LogHelper
    {
        /// <summary>
        /// 日志写入
        /// </summary>
        /// <param name="enumType">日志类型</param>
        /// <param name="str">日志描述</param>
        public static void WriteLog(LogEnum enumType, object str)
        {
            try
            {
                string fileName = string.Format("/{0}-{1}-{2}.txt", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                string path = "~/Logs/" + enumType;
                if (enumType == LogEnum.Error)
                {
                    str = "\r\n" + Utils.GetCurrentUrl() + "    " + DateTime.Now.ToString("HH时mm分ss秒") + "\r\n" + str;
                }
                else
                {
                    str = "\r\n" + DateTime.Now.ToString("HH时mm分ss秒") + "   " + str;
                }
                WriteTxt(str.ToString(), path, fileName);
            }
            catch
            {
            }
        }


        /// <summary>
        /// 写调试日志 相对路径
        /// </summary>
        /// <param name="str"></param>
        /// <param name="filepath"></param>
        /// <returns></returns>
        private static void WriteTxt(string str, string filepath, string filename)
        {

            filepath = HttpContext.Current.Request.MapPath(filepath);
            filename = filepath + filename;
            if (!Directory.Exists(filepath))
            {
                System.IO.Directory.CreateDirectory(filepath);
            }
            FileStream fs = new FileStream(filename, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            //开始写入
            sw.WriteLine(str);
            //清空缓冲区
            sw.Flush();
            //关闭流
            sw.Close();
            fs.Close();
        }
    }
}
