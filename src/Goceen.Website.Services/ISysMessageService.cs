using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Leon.Core.Services;
using Leon.Data;
using Goceen.Website.Domain;
namespace Goceen.Website.Services
{
    public interface ISysMessageService: IGenericService<SysMessage>
    {
        void Delete(IList<int> idList);

        IList<SysMessage> LoadAllByPage(out long total, int page, int rows, string order, string sort);

        IList<SysMessage> LoadAllReviewByPage(out long total, int page, int rows, string order, string sort);

        bool IsExist(int MessageId);

        IList<SysMessage> LoadAllReview(int MessageId);

        IList<SysMessage> LoadAllReview();

        //void ViewsAdd(int id);
        IList<SysMessage> LoadTopList(int num, int MessageId);
    }
}
