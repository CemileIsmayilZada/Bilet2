using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anyar.Business.ViewModels.Auth
{
    public class LoginViewModel
    {
        public string? UsernameOrEmail { get; set; }
        public string? Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
