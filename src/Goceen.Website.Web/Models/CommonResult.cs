using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Goceen.Website.Web
{
    [Serializable]
    public class CommonResult
    {
        /// <summary>
        /// 操作成功
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 消息列表
        /// </summary>
        public Dictionary<string, string> MessageLsit { get; set; }

        public CommonResult()
        {
            MessageLsit = new Dictionary<string, string>();
        }
    }
}