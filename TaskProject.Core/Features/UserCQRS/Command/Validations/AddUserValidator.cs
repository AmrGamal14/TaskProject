using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskProject.Core.Features.UserCQRS.Command.Models;

namespace TaskProject.Core.Features.UserCQRS.Command.Validations
{
    public class AddUserValidator : AbstractValidator<AddUserCommand>
    {
        #region Fields
        #endregion

        #region constructors
        public AddUserValidator()
        {
            ApplyValidationsRules();
            ApplyCustomValidationsRules();
        }
        #endregion

        #region Handle Functions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("FullName Must not Be Empty");

            RuleFor(x => x.UserName)
                .NotNull().WithMessage("UserName Must not Be Null");
            RuleFor(x => x.Email)
               .NotNull().WithMessage("Email Must not Be Null");
            RuleFor(x => x.Password)
               .NotNull().WithMessage("Password Must not Be Null");
            RuleFor(x => x.ConfirmPassword)
               .NotNull().WithMessage("ConfirmPassword Must not Be Null")
               .Equal(x=>x.Password).WithMessage("Password Not Equal ConfirmPassword");

        }
        public void ApplyCustomValidationsRules()
        {

        }
        #endregion

    }
}
