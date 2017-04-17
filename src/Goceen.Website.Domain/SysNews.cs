using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goceen.Website.Domain
{
    public class SysNews: BaseEntity
    {
        /// <summary>
        /// 标题
        /// </summary>
        public virtual string Title { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public virtual int OrderNo { get; set; }
        /// <summary>
        /// 链接
        /// </summary>
        public virtual string Url { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public virtual bool IsEnabled { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateDate { get; set; }

        /// <summary>
        /// 更改时间
        /// </summary>
        public virtual DateTime UpdateDate { get; set; }

    }
}
