using Common.Utils;
using FluentValidation;
using WebApp.Models.MedicineModels;

namespace WebApp.Validators
{
    public class CreateMedicineViewModelValidator : AbstractValidator<CreateMedicineViewModel>
    {
        public CreateMedicineViewModelValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Amount).GreaterThan(0)
                .WithMessage("Ilość leku w opakowaniu musi być dodatnia");

            RuleFor(x => x.Name).NotEmpty()
                .WithMessage("Lek musi posiadać nazwę");

            RuleFor(x => x.UnitPrice)
                .GreaterThan(0)
                .WithMessage("Cena leku musi być dodatnia");
            
            RuleFor(x => x.RefundString)
                .Matches(@"^\s*\d+([,.]\d+)?\s*%?\s*$")
                .When(x => !string.IsNullOrEmpty(x.RefundString))
                .WithMessage("Niewłaściwy format refundacji. Prykładowe wartości to: 20%; 20; 20,0 lub brak wartości w przypadku braku refundacji.")
                .Must(x => x.IsValidPercent())
                .WithMessage("Niewłaściwy format refundacji")
            ;
        }
    }
}
