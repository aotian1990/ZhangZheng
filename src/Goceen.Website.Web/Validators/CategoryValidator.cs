using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using Goceen.Website.Domain;

namespace Goceen.Website.Web.Validators
{
    public class CategoryValidator : AbstractValidator<SysCategory>
    {
        public CategoryValidator()
        {
            RuleFor(t => t.Name).NotEmpty().WithName("分类名称");
            RuleFor(t => t.OrderNo).InclusiveBetween(0, 999).WithMessage("排列序号必须是0-999之间");
        }
    }
}