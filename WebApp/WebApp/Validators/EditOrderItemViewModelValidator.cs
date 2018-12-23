using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models.OrderModels;

namespace WebApp.Validators
{
    public class EditOrderItemViewModelValidator : AbstractValidator<EditOrderItemViewModel>
    {
        public EditOrderItemViewModelValidator()
        {
            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithMessage("Nowa ilość leku musi być większa od zera");
        }
    }
}
