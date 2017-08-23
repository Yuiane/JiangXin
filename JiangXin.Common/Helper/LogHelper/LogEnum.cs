using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangXin.Common
{
    public enum LogEnum
    {
        /// <summary>
        /// 订单日志记录
        /// </summary>
        WeChatSend,

        /// <summary>
        /// 异常日志
        /// </summary>
        Error,


        /// <summary>
        /// CCCAPI日志
        /// </summary>
        CCCApi,

        /// <summary>
        /// CCCAPI日志
        /// </summary>
        CCCApiJson
    }
}
