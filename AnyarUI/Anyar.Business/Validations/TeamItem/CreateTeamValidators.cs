using Anyar.Business.ViewModels.TeamViewModel;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anyar.Business.Validations.TeamItem
{
    public class CreateTeamValidators: AbstractValidator<CreateTeamItemVm>
    {
        public CreateTeamValidators()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .MaximumLength(50);
            RuleFor(x => x.Description)
                .NotNull()
                .NotEmpty()
                .MaximumLength(250);
            RuleFor(x => x.Position)
                .NotEmpty()
                .NotNull()
                .MaximumLength(50);
            RuleFor(x => x.Image)
                .NotNull();
                
        }
    }
}
