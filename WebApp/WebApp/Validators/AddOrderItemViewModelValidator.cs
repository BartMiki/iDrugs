using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models.OrderModels;

namespace WebApp.Validators
{
    public class AddOrderItemViewModelValidator : AbstractValidator<AddOrderItemViewModel>
    {
        public AddOrderItemViewModelValidator()
        {
            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithMessage("Zamawiana liczba danego leku musi być większa od zera")
                ;
        }
    }
}
