using BlogApp.BLL.RequestModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data;
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

        public bool ExistingRole(string roleName)
        {
            for(int i = 0;i < RoleValues.Roles.Count();i++)
            {
                if (roleName == RoleValues.Roles[i])
                    return true;
            }
            return false;
        }
    }

    //public class ExistingRole :Rule
    //{
    //    public ExistingRole()
    //    {
    //        for (int i = 0; i < RoleValues.Roles.Count(); i++)
    //        {
    //            if (roleName == RoleValues.Roles[i])
    //                return true;
    //        }
    //        return false;
    //    }
    //}
}
