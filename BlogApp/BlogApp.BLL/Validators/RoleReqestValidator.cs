using BlogApp.BLL.RequestModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.BLL.Validators
{
    public class RoleReqestValidator : AbstractValidator<RoleReqest>
    {
        public RoleReqestValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
