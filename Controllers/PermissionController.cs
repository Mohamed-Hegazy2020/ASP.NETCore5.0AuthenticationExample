using ASP.NETCore5._0AuthenticationExample.Constants;
using ASP.NETCore5._0AuthenticationExample.Helpers;
using ASP.NETCore5._0AuthenticationExample.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace ASP.NETCore5._0AuthenticationExample.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class PermissionController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public PermissionController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<ActionResult> Index(string roleId)
        {
            var model = new PermissionViewModel();
            var allPermissions = new List<RoleClaimsViewModel>();
            allPermissions.GetPermissions(typeof(Permissions.Products), roleId);
            var role = await _roleManager.FindByIdAsync(roleId);
            model.RoleId = roleId;
            model.RoleName = role.Name;
            var claims = await _roleManager.GetClaimsAsync(role);
            var allClaimValues = allPermissions.Select(a => a.Value).ToList();
            var roleClaimValues = claims.Select(a => a.Value).ToList();
            var authorizedClaims = allClaimValues.Intersect(roleClaimValues).ToList();
            foreach (var permission in allPermissions)
            {
                if (authorizedClaims.Any(a => a == permission.Value))
                {
                    permission.Selected = true;
                }
            }
            model.RoleClaims = allPermissions;
            return View(model);
        }


      


        public async Task<IActionResult> Update(PermissionViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.RoleId);
            var claims = await _roleManager.GetClaimsAsync(role);
            foreach (var claim in claims)
            {
                await _roleManager.RemoveClaimAsync(role, claim);
            }
            var selectedClaims = model.RoleClaims.Where(a => a.Create || a.View||a.Edit||a.Delete).ToList();
            foreach (var claim in selectedClaims)
            {
                if (claim.Create)
                {
                    await _roleManager.AddPermissionClaim(role, claim.CreateValue);
                }
                if (claim.View)
                {
                    await _roleManager.AddPermissionClaim(role, claim.ViewValue);
                }
                if (claim.Edit)
                {
                    await _roleManager.AddPermissionClaim(role, claim.EditValue);
                }
                if (claim.Delete)
                {
                    await _roleManager.AddPermissionClaim(role, claim.DeleteValue);
                }

            }
            return RedirectToAction("Index", "Roles", new { roleId = model.RoleId });
        }
    }
}
