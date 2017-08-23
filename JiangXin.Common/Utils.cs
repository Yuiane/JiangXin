using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace JiangXin.Common
{
    public class Utils
    {

        /// <summary>
        /// 得到当前访问的路径[http://www.baidu.com?pIdx=2]
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentUrl()
        {
            return HttpContext.Current.Request.Url.ToString();
        }
    }
}
