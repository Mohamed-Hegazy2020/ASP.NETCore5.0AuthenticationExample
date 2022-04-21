using ASP.NETCore5._0AuthenticationExample.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using ASP.NETCore5._0AuthenticationExample.ViewModels;
using System;
using System.Collections.Generic;

namespace ASP.NETCore5._0AuthenticationExample.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }
        [HttpPost]
        public async Task<IActionResult> AddRole(string roleName)
        {
            if (roleName != null)
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName.Trim()));
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ManagePermissions(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);

            if (role == null)
                return NotFound();

            var roleClaims = _roleManager.GetClaimsAsync(role).Result.Select(c => c.Value).ToList();
            var allClaims = Permissions.GenerateAllPermissions();
            var allPermissions = allClaims.Select(p => new RoleClaimsViewModel { Value = p, Type = "Permission" }).ToList();

            //foreach (var permission in allPermissions)
            //{
            //    if (roleClaims.Any(c => c == permission.Value))
            //        permission.Selected = true;
            //}


            var modules = Enum.GetValues(typeof(Modules));
            List<RoleClaimsViewModel> RoleClaimsViewModel = new List<RoleClaimsViewModel>();
            foreach (var m in modules)
            {
                RoleClaimsViewModel p = new RoleClaimsViewModel();
                p.ModuleName = m.ToString();
                if (roleClaims.Any(c => c == $"Permissions.{m}.Create"))
                {                   
                    p.Create =true;
                    p.CreateValue = $"Permissions.{m}.Create";
                }
                else
                {
                    p.Create = false;
                    p.CreateValue = $"Permissions.{m}.Create";
                }
                if (roleClaims.Any(c => c == $"Permissions.{m}.View"))
                {                   
                    p.View = true;
                    p.ViewValue = $"Permissions.{m}.View";
                }
                else
                {
                    p.View = false;
                    p.ViewValue = $"Permissions.{m}.View";
                }
                if (roleClaims.Any(c => c == $"Permissions.{m}.Edit"))
                {                  
                    p.Edit = true;
                    p.EditValue = $"Permissions.{m}.Edit";
                }
                else
                {
                    p.Edit = false;
                    p.EditValue = $"Permissions.{m}.Edit";
                }
                if (roleClaims.Any(c => c == $"Permissions.{m}.Delete"))
                {                  
                    p.Delete = true;
                    p.DeleteValue = $"Permissions.{m}.Delete";
                }
                else
                {
                    p.Delete = false;
                    p.DeleteValue = $"Permissions.{m}.Delete";
                }
                RoleClaimsViewModel.Add(p);
            }
            var viewModel = new PermissionViewModel
            {
                RoleId = roleId,
                RoleName = role.Name,
                RoleClaims = RoleClaimsViewModel
            };

            return View(viewModel);
        }

        public  IActionResult Settings()
        {           
            return View();
        }
    }
}
