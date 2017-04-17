using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using Goceen.Website.Domain;

namespace Goceen.Website.Web.Validators
{
    public class ChannelValidator : AbstractValidator<SysChannel>
    {
        public ChannelValidator()
        {
            RuleFor(t => t.Name).NotEmpty().WithName("频道名称");
            RuleFor(t => t.OrderNo).InclusiveBetween(0, 999).WithMessage("排列序号必须是0-999之间");
            RuleFor(t => t.Keyword).NotEmpty().WithName("关键字");
            RuleFor(t => t.Url).NotEmpty().WithName("链接地址");
        }
    }
}