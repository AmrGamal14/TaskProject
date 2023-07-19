﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskProject.Core.Features.Products.Commands.Models;

namespace TaskProject.Core.Features.Products.Commands.Validations
{
    public class EditProductValidatior : AbstractValidator<EditProductCommand>
    {
        #region
        public EditProductValidatior()
        {
            ApplyValidationsRules();
            ApplyCustomValidationsRules();

        }
         #endregion
        #region Actions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name Must not Be Empty");

            RuleFor(x => x.Price)
                .NotNull().WithMessage("Price Must not Be Null");
        }
        public void ApplyCustomValidationsRules()
        {

        }
        #endregion
    }
}
