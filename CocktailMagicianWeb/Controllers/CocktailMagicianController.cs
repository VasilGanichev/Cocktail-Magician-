using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CocktailMagician.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CocktailMagicianWeb.Controllers
{
    public class CocktailMagicianController : Controller
    {
        private readonly UserManager<User> userManager;

        public CocktailMagicianController(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> ManageUsers(ManageUserViewModel manageUsers)
        {
            List<User> users;
            IList<string> roles;
            ManageUserViewModel manageUserVm;
            List<UserViewModel> usersVm = new List<UserViewModel>();

            if (string.IsNullOrEmpty(manageUsers.Input))
            {
                return View();
            }

            users = await userManager.Users.Where(u => u.UserName.Contains(manageUsers.Input)).ToListAsync();
            if (users.Count == 0)
            {
                ModelState.AddModelError(string.Empty, "No user found with this name.");
                manageUserVm = new ManageUserViewModel();
                return View(manageUserVm);
            }

            foreach (var user in users)
            {
                roles = await userManager.GetRolesAsync(user);
                var userRole = roles.OrderBy(r => r).First();
                usersVm.Add(new UserViewModel
                {
                    UserId = user.Id,
                    Name = user.UserName,
                    Role = userRole,
                    IsBanned = user.IsBanned
                });
            }
            manageUserVm = new ManageUserViewModel(usersVm);
            return View(manageUserVm);
        }
        public async Task Ban([FromBody]UserDataViewModel userData)
        {
            var user = await userManager.FindByIdAsync(userData.UserId);
            user.IsBanned = true;
            await userManager.UpdateAsync(user);
        }
        public async Task RemoveBan([FromBody]UserDataViewModel userData)
        {
            var user = await userManager.FindByIdAsync(userData.UserId);
            user.IsBanned = false;
            await userManager.UpdateAsync(user);
        }
        public async Task Promote([FromBody]UserDataViewModel userData)
        {
            var user = await userManager.FindByIdAsync(userData.UserId);
            await userManager.AddToRoleAsync(user, "Librarian");
        }
        public async Task Demote([FromBody]UserDataViewModel userData)
        {
            var user = await userManager.FindByIdAsync(userData.UserId);
            await userManager.RemoveFromRoleAsync(user, "Librarian");
        }
    }
}