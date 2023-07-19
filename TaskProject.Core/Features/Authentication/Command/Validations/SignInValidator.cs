using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskProject.Core.Features.Authentication.Command.Models;
using TaskProject.Core.Features.Products.Commands.Models;

namespace TaskProject.Core.Features.Authentication.Command.Validations
{
    public class SignInValidator : AbstractValidator<SignInCommand>

    {
        #region Fields
        public SignInValidator()
        {
            ApplyValidationsRules();
            ApplyCustomValidationsRules();

        }
        #endregion
        #region Actions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("UserName Must not Be Empty")
                .NotNull().WithMessage("UserName Must not Be Null");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password Must not Be Empty")
                .NotNull().WithMessage("Password Must not Be Null");
        }
        public void ApplyCustomValidationsRules()
        {

        }
        #endregion
    }
}
