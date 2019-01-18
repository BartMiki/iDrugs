using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models.PrescriptionModels;

namespace WebApp.Validators
{
    public class CreatePrescriptionModelValidaotr : AbstractValidator<CreatePrescriptionViewModel>
    {
        public CreatePrescriptionModelValidaotr()
        {
            RuleFor(x => x.Email).EmailAddress().WithMessage("Adres email jest niepoprawny");
            RuleFor(x => x.PrescriptionDate).LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Data niepoprawna, recepta nie może być przyszłości");
        }
    }
}
