using Anyar.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anyar.Business.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<TeamItem>? TeamItems { get; set; }
    }
}
