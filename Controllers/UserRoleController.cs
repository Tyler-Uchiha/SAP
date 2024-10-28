using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using SAP.Models;
using SAP.Utility;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;

namespace SAP.Controllers
{
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Super_User)]
    public class UserRoleController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRoleController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        private void StatsUserRole() 
        {

            var users = _userManager.Users.ToList();

            // Count users in each role using LINQ
            var caseManagersCount = users.Count(u => _userManager.GetRolesAsync(u).Result.Contains(SD.Role_Case_Manager));
            var stationManagersCount = users.Count(u => _userManager.GetRolesAsync(u).Result.Contains(SD.Role_Station_Manager));
            var policeOfficersCount = users.Count(u => _userManager.GetRolesAsync(u).Result.Contains(SD.Role_Police));


            //var caseManagers = SD.Role_Case_Manager.Count();
            //var stationManagers = SD.Role_Station_Manager;
            //var policeOfficers = SD.Role_Police;

            ViewBag.CaseManagers = caseManagersCount;
            ViewBag.StationManagersCount = stationManagersCount;
            ViewBag.PoliceOfficersCount = policeOfficersCount;   
        }

        public async Task<IActionResult> UpdateUserRoles()
        {
            
            var users = _userManager.Users.ToList();
            var model = new List<UpdateUserRolesViewModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var role = roles.FirstOrDefault();
                model.Add(new UpdateUserRolesViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    FirstName = user.First_Name,
                    LastName = user.Last_Name,
                    CurrentRole = role,
                    Roles = _roleManager.Roles.ToList()
                });
            }

            StatsUserRole();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUserRoles(string userId, string newRole)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var currentRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, currentRoles);
                await _userManager.AddToRoleAsync(user, newRole);
                
                //Update the ASPNET.USERS table 
                user.User_Role = newRole;
                user.User_Role_Identifier = (await _roleManager.FindByNameAsync(newRole)).Id;

                // Save changes to the database
                var updateResult = await _userManager.UpdateAsync(user);
            }

            return RedirectToAction("UpdateUserRoles");
        }
    }
}






























//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Identity;
//using SAP.Models;
//using SAP.Utility;
//using Microsoft.AspNetCore.Authorization;
//using System.Linq;
//using System.Threading.Tasks;

//namespace SAP.Controllers
//{
//    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Super_User)]
//    public class UserRoleController : Controller
//    {
//        private readonly UserManager<ApplicationUser> _userManager;
//        private readonly RoleManager<IdentityRole> _roleManager;

//        public UserRoleController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
//        {
//            _userManager = userManager;
//            _roleManager = roleManager;
//        }

//        public async Task<IActionResult> UpdateUserRoles()
//        {
//            var users = _userManager.Users.ToList();
//            var model = new List<UpdateUserRolesViewModel>();

//            foreach (var user in users)
//            {
//                var roles = await _userManager.GetRolesAsync(user);
//                var role = roles.FirstOrDefault();
//                model.Add(new UpdateUserRolesViewModel
//                {
//                    UserId = user.Id,
//                    UserName = user.UserName,
//                    CurrentRole = role,
//                    Roles = _roleManager.Roles.ToList()
//                });
//            }

//            return View(model);
//        }

//        [HttpPost]
//        public async Task<IActionResult> UpdateUserRoles(string userId, string newRole)
//        {
//            var user = await _userManager.FindByIdAsync(userId);
//            if (user != null)
//            {
//                var currentRoles = await _userManager.GetRolesAsync(user);
//                await _userManager.RemoveFromRolesAsync(user, currentRoles);
//                await _userManager.AddToRoleAsync(user, newRole);
//            }

//            return RedirectToAction("UpdateUserRoles");
//        }
//    }
//}

