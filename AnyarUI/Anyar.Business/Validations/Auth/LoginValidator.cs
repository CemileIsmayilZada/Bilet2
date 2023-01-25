using Anyar.Business.ViewModels.Auth;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anyar.Business.Validations.Auth
{
    public class LoginValidator:AbstractValidator<LoginViewModel>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Password)
              .NotEmpty()
              .NotNull();
            RuleFor(x => x.UsernameOrEmail)
               .NotEmpty()
               .NotNull()
               .MaximumLength(50);
        }
    }
}
