using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goceen.Website.Domain
{
    public class SysChannel : BaseEntity
    {
        /// <summary>
        /// 排列序号
        /// </summary>
        public virtual int OrderNo { get; set; }

        /// <summary>
        /// 频道名称
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual bool IsEnabled { get; set; }

        /// <summary>
        /// 频道链接
        /// </summary>
        public virtual string Url { get; set; }

        /// <summary>
        /// 频道内容
        /// </summary>
        public virtual string Content { get; set; }

        /// <summary>
        /// 关键字
        /// </summary>
        public virtual string Keyword { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public virtual string Description { get; set; }

        public virtual IList<SysCategory> Categorys { get; set; }
    }
}
