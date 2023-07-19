using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskProject.Core.Features.UserCQRS.Command.Models;

namespace TaskProject.Core.Features.UserCQRS.Command.Validations
{
    public class ChangeUserPasswordValidator : AbstractValidator<ChangeUserPasswordCommand>
    {
        public ChangeUserPasswordValidator()
        {
            ApplyValidationsRules();
            ApplyCustomValidationsRules();
        }
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id Must not Be Empty")
                .NotNull().WithMessage("Id Must not Be Null");


            RuleFor(x => x.CurrentPassword)
                .NotEmpty().WithMessage("CurrentPassword Must not Be Empty")
                .NotNull().WithMessage("CurrentPassword Must not Be Null"); 
            RuleFor(x => x.NewPassword)
               .NotEmpty().WithMessage("NewPassword Must not Be Empty")
               .NotNull().WithMessage("NewPassword Must not Be Null"); 
            RuleFor(x => x.ConfirmPassword)
               .NotEmpty().WithMessage("ConfirmPassword Must not Be Empty")
               .NotNull().WithMessage("ConfirmPassword Must not Be Null")
               .Equal(x => x.NewPassword).WithMessage("Password Not Equal ConfirmPassword"); 
        
        }
    public void ApplyCustomValidationsRules()
        {

        }
    }
}
