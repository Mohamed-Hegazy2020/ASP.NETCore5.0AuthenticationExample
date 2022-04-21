using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NETCore5._0AuthenticationExample.ViewModels
{
    public class ManageUserRolesViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public IList<UserRolesViewModel> UserRoles { get; set; }
    }

    public class UserRolesViewModel
    {
        public string RoleName { get; set; }
        public bool Selected { get; set; }
    }
}
