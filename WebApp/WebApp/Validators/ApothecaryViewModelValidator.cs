using FluentValidation;
using WebApp.Models;

namespace WebApp.Validators
{
    public class ApothecaryViewModelValidator : AbstractValidator<ApothecaryViewModel>
    {
        public ApothecaryViewModelValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Imię jest wymagane")
                .MaximumLength(50).WithMessage("Imię nie może być dłuższe niż 50 liter")
                .NotEqual("Adolf").WithMessage("Nie zatrudniamy Adolfów");
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Nazwisko jest wymagane")
                .MaximumLength(50).WithMessage("Nazwisko nie może być dłuższe niż 50 liter");
            RuleFor(x => x.MonthlySalary)
                .GreaterThan(0).WithMessage("Pensja musi być dodatnia");
        }
    }
}
