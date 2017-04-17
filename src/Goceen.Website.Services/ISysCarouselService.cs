using System.Linq;
using System.Text;
using System.Collections.Generic;
using Leon.Core.Services;
using Leon.Data;
using Goceen.Website.Domain;


namespace Goceen.Website.Services
{
    public interface ISysCarouselService : IGenericService<SysCarousel>
    {
        void Delete(IList<int> idList);

        IList<SysCarousel> LoadAllByPage(out long total, int page, int rows, string order, string sort);

        bool IsExist(int CarouselId);

        IList<SysCarousel> LoadAllEnable(int CarouselId);

        IList<SysCarousel> LoadAllEnable();
        //void ViewsAdd(int id);
        IList<SysCarousel> LoadTopList(int num, int CarouselId);
    }
}
