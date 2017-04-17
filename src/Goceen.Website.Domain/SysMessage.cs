using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goceen.Website.Domain
{
    public class SysMessage: BaseEntity
    {
        /// <summary>
        /// 称呼名
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public virtual int OrderNo { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public virtual string Phone { get; set; }
        /// <summary>
        /// 留言内容
        /// </summary>
        public virtual string Content { get; set; }
        /// <summary>
        /// 是否审核
        /// </summary>
        public virtual bool IsReview { get; set; }
        /// <summary>
        /// 管理员回复内容
        /// </summary>
        public virtual string Reply { get; set; }

        public virtual DateTime CreateDate { get; set; }


        public virtual DateTime UpdateDate { get; set; }



    }
}
