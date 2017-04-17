using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using Goceen.Website.Domain;

namespace Goceen.Website.Web.Validators
{
    public class Accountvalidator : AbstractValidator<SysUser>
    {
        public Accountvalidator()
        {
            RuleFor(t => t.Name).NotEmpty().WithName("姓名").Length(2,20);
            RuleFor(t => t.Password).Length(6, 20).WithMessage("密码在6-20位之间");
            RuleFor(t => t.Account).NotEmpty().WithName("帐号").Length(4, 20);
        }
    }
}