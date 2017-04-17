using System.Linq;
using System.Text;
using System.Collections.Generic;
using Leon.Core.Services;
using Leon.Data;
using Goceen.Website.Domain;
namespace Goceen.Website.Services
{
    public interface ISysNewsService : IGenericService<SysNews>
    {
        void Delete(IList<int> idList);

        IList<SysNews> LoadAllByPage(out long total, int page, int rows, string order, string sort);

        bool IsExist(int MessageId);

        IList<SysNews> LoadAllEnable(int NewsId);

        IList<SysNews> LoadAllEnable();
        //void ViewsAdd(int id);
        IList<SysNews> LoadTopList(int num, int NewsId);
    }
}
