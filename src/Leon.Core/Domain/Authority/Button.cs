using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leon.Core.Domain.Authority
{
    public class Button : EntityBase
    {
        /// <summary>
        /// 按钮名称
        /// </summary>
        public virtual string ButtonName { get; set; }

        /// <summary>
        /// 按钮代码
        /// </summary>
        public virtual string ButtonCode { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public virtual int Sort { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// 按钮图标
        /// </summary>
        public virtual string ButtonIcon { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateDate { get; set; }
    }
}
