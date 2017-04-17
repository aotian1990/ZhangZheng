using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using Goceen.Website.Domain;

namespace Goceen.Website.Web.Validators
{
    public class ConfigValidator : AbstractValidator<SysConfig>
    {
        public ConfigValidator()
        {
            RuleFor(t => t.WebTitle).NotEmpty();
            RuleFor(t => t.WebUrl).NotEmpty();

        }
    }
}