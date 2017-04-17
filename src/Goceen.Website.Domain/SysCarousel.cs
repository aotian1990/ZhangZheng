using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goceen.Website.Domain
{
    public class SysCarousel:BaseEntity
    {
        public virtual string Title { get; set; }

        public virtual string Url { get; set; }

        public virtual string Img { get; set; }

        public virtual int OrderNo { get; set; }

        public virtual bool IsEnabled { get; set; }
    }
}
