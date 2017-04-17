using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leon.Core.Extensions;

namespace Goceen.Website.Domain
{
    public class SysCategory : BaseEntity
    {
        public virtual int OrderNo { get; set; }

        public virtual string Name { get; set; }

        public virtual string Content { get; set; }

        public virtual string Keyword { get; set; }

        public virtual string Description { get; set; }

        public virtual bool IsEnabled { get; set; }

        public virtual CategoryType CategoryType { get; set; }

        public virtual bool IsImgList { get; set; }

        public virtual string Url { get; set; }

        public virtual SysChannel Channel { get; set; }

        public virtual IList<SysArticle> Articles { get; set; }
    }

    public enum CategoryType
    {
        [EnumDisplayName("列表")]
        list = 2,
        [EnumDisplayName("内容")]
        content = 4,
        [EnumDisplayName("链接")]
        url = 8
    }
}
