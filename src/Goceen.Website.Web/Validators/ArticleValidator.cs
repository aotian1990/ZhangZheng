using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using Goceen.Website.Domain;

namespace Goceen.Website.Web.Validators
{
    public class ArticleValidator :AbstractValidator<SysArticle>
    {
        public ArticleValidator()
        {
            RuleFor(t => t.Title).NotEmpty().WithName("标题");
        }
    }
}