using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Student_Management_System.Models.ViewModel;

namespace Student_Management_System.Controllers
{
    [Authorize(Roles ="admin")] //+Role Admin

    public class RoleController : Controller
    {
        RoleManager<IdentityRole> roleManager;
        public RoleController(RoleManager<IdentityRole> _roleManager) 
        {
            roleManager = _roleManager;
        }
        public IActionResult AddRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddRole(RoleViewModel roleViewModel)
        {
            if(ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole() { Name=roleViewModel.RoleName };

                IdentityResult result =  await roleManager.CreateAsync(identityRole);
                if(result.Succeeded)
                {
                    return View();

                }
                else
                {
                    foreach(var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View();
        }


    }
}
