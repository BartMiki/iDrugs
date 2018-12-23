using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models.WarehouseModels;

namespace WebApp.Validators
{
    public class AddWarehouseItemViewModelValidator : AbstractValidator<AddWarehouseItemViewModel>
    {
        public AddWarehouseItemViewModelValidator()
        {
            RuleFor(x => x.Quantity)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Wymagana wartość większą lub równa od zera");
        }
    }
}
