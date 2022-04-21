using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NETCore5._0AuthenticationExample.Constants
{
    public static class Permissions
    {
        public static List<string> GeneratePermissionsForModule(string module)
        {
            return new List<string>()
            {
              $"Permissions.{module}.Create",
              $"Permissions.{module}.View",
              $"Permissions.{module}.Edit",
              $"Permissions.{module}.Delete",
            };
        }

        public static List<string> GenerateAllPermissions()
        {
            var allPermissions = new List<string>();

            var modules = Enum.GetValues(typeof(Modules));

            foreach (var module in modules)
                allPermissions.AddRange(GeneratePermissionsForModule(module.ToString()));

            return allPermissions;
        }

        public static class Products
        {
            public const string View = "Permissions.Products.View";
            public const string Create = "Permissions.Products.Create";
            public const string Edit = "Permissions.Products.Edit";
            public const string Delete = "Permissions.Products.Delete";
        }

     
    }
}
