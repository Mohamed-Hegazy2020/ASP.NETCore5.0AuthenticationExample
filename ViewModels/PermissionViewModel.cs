using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NETCore5._0AuthenticationExample.ViewModels
{
    public class PermissionViewModel
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public IList<RoleClaimsViewModel> RoleClaims { get; set; }
    }

    public class RoleClaimsViewModel
    {
        public string Type { get; set; }
        public string Value { get; set; }
        public bool Selected { get; set; }

        public string ModuleName { get; set; }
        public bool Create { get; set; }
        public bool View { get; set; }
        public bool Edit { get; set; }
        public bool Delete { get; set; }

        public string CreateValue { get; set; }
        public string ViewValue { get; set; }
        public string EditValue { get; set; }
        public string DeleteValue { get; set; }

    }
}
