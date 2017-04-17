using System.Linq;
using System.Text;
using System.Collections.Generic;
using Leon.Core.Services;
using Leon.Data;
using Goceen.Website.Domain;


namespace Goceen.Website.Services
{
    public interface ISysIconService: IGenericService<SysIcon>
    {
        void Delete(IList<int> idList);

        IList<SysIcon> LoadAllByPage(out long total, int page, int rows, string order, string sort);

        bool IsExist(int IconId);

        IList<SysIcon> LoadAllEnable(int IconId);

        IList<SysIcon> LoadAllEnable();
        //void ViewsAdd(int id);
        IList<SysIcon> LoadTopList(int num, int IconId);
    }
}
