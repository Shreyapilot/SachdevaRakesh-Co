using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SachdevaCo.Core.Model.ViewModels
{
    public class LoginRegisterViewModel
    {
      
            public LoginViewModel Login { get; set; } = new();
            public RegisterViewModel Register { get; set; } = new();
        }

    

}
