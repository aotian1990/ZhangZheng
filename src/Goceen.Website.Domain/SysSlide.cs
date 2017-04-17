using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goceen.Website.Domain
{
    public class SysSlide : BaseEntity
    {
        
        public virtual string ImgUrl { get; set; }

        public virtual string ImgTitle { get; set; }

        public virtual string LinkUrl { get; set; }

        public virtual int OrderNo { get; set; }
    }
}
