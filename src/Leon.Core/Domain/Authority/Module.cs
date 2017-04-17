using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leon.Core.Domain.Authority
{
    public class Module : TreeNode
    {
        /// <summary>
        /// 排列顺序
        /// </summary>
        public virtual int Sort { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public virtual string Icon { get; set; }

        /// <summary>
        /// 是否禁用
        /// </summary>
        public virtual bool IsEnable { get; set; }

        /// <summary>
        /// 是否菜单
        /// </summary>
        public virtual bool IsMenu { get; set; }

        /// <summary>
        /// 请求地址
        /// </summary>
        public virtual string ActionName { get; set; }

        /// <summary>
        /// 请求地址
        /// </summary>
        public virtual string ControllerName { get; set; }



    }
}
