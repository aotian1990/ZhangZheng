﻿// <autogenerated>
//   This file was generated by T4 code generator NhibernateMappingOutput.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Leon.Core.Services;
using Leon.Data;
using Goceen.Website.Domain;

namespace Goceen.Website.Services
{
    public interface ISysArticleService : IGenericService<SysArticle>
	{      

        void Delete(IList<int> idList);
         
        IList<SysArticle> LoadAllByPage(out long total, int page, int rows, string order, string sort);

        IList<SysArticle> LoadAllByPage(out long total, int categoryId, int page, int rows, string order, string sort);

        bool IsExist(string title);

        IList<SysArticle> LoadAllEnable(int categoryId);

        void ViewsAdd(int id);


        IList<SysArticle> LoadTopList(int num, int categoryId);

	}
}