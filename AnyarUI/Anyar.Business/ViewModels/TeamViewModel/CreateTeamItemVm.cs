using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anyar.Business.ViewModels.TeamViewModel
{
    public class CreateTeamItemVm
    {
        public string? Name { get; set; }
        public string? Position { get; set; }
        public string? Description { get; set; }
        public IFormFile? Image { get; set; }
    }
}
