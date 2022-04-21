using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NETCore5._0AuthenticationExample.ViewModels
{
    public class LoginViewModel
    {

        [Required(ErrorMessage = "Please Enter Email..")]
        [Display(Name = "Emaile")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Enter Password...")]
        [DataType(DataType.Password)]       
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }


    }
}
