using Anyar.Business.ViewModels.Auth;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anyar.Business.Validations.Auth
{
    public class RegisterValidator : AbstractValidator<RegisterViewModel>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .NotNull()
                .MaximumLength(50);
            RuleFor(x => x.Fullname)
                .NotEmpty()
                .NotNull()
                .MaximumLength(50);
            RuleFor(x => x.Email)
            .NotEmpty()
            .NotNull()
            .MaximumLength(50)
            .EmailAddress();
            RuleFor(x => x.Password)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.ConfirmedPassword)
             .NotEmpty()
             .NotNull()
             .Equal(x => x.Password);

        }
    }
}
