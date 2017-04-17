using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leon.Core.Services;
using Leon.Data;
using Goceen.Website.Domain;

namespace Goceen.Website.Services
{
    public interface ISysSlideService : IGenericService<SysSlide>
    {
        IList<SysSlide> LoadAllByPage(out long total, int page, int rows, string order, string sort);

        IList<SysSlide> LoadAllEnable();

        void Delete(IList<int> idList);
    }
}
