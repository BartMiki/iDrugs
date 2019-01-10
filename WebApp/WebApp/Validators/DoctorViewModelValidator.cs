using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models.DoctorModels;

namespace WebApp.Validators
{
    public class DoctorViewModelValidator : AbstractValidator<DoctorViewModel>
    {
        public DoctorViewModelValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Trzeba podać imię");

            RuleFor(x => x.LastName).NotEmpty().WithMessage("Trzeba podać nazwisko");
        }
    }
}
