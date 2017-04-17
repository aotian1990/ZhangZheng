using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goceen.Website.Domain
{
    public class SysArticle : BaseEntity
    {
        public virtual string Title { get; set; }


        public virtual string Content { get; set; }


        public virtual string Keyword { get; set; }


        public virtual string Description { get; set; }


        public virtual bool IsEnabled { get; set; }


        public virtual bool IsFirst { get; set; }


        public virtual int ViewCount { get; set; }


        public virtual string RecommendImg { get; set; }


        public virtual DateTime CreateDate { get; set; }


        public virtual DateTime UpdateDate { get; set; }


        public virtual SysCategory Category { get; set; }
    }
}
