using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PuzzleStore.Web.Infrastructure;
using PuzzleStore.Services.Admin;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using PuzzleStore.Web.Areas.Admin.Models;
using PuzzleStore.Data.Models;
using Microsoft.EntityFrameworkCore;
using PuzzleStore.Web.Infrastructure.Extensions;

namespace PuzzleStore.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = GlobalConstants.AdministratorRole)]
    public class UsersController : Controller
    {
        private readonly IAdminUserService users;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;

        public UsersController(IAdminUserService users,
                RoleManager<IdentityRole> roleManager
            , UserManager<User> userManager)
        {
            this.users = users;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await this.users.AllAsync();
            var roles = await this.roleManager
                .Roles
                .Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Name
                })
                .ToListAsync();

            return View(new AdminUsersListingViewModel
            {
                Users = users,
                Roles = roles
            });
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await this.userManager.CreateAsync(new User
            {
                UserName = model.Username,
                Email = model.Email
            }, model.Password);

            if (result.Succeeded)
            {
                TempData["SuccesMessage"] = $"User with username {model.Username} created.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                this.AddModelErrors(result);
                return View(model);
            }
        }

        public async Task<IActionResult> ChangePassword(string id)
        {
            var user = await this.userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(new IdentityChangePasswordViewModel
            {
                Username = user.UserName
            });
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(string id, IdentityChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await this.userManager.FindByIdAsync(id);
            var token = await this.userManager.GeneratePasswordResetTokenAsync(user);
            var result = await this.userManager.ResetPasswordAsync(user, token, model.Password);

            if (result.Succeeded)
            {
                TempData["SuccesMessage"] = $"Password changed for user{user.UserName}";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                this.AddModelErrors(result);
                return this.View(model);
            }
        }

        public async Task<IActionResult> Delete(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return this.View(new DeleteUserViewModel
            {
                Id = id,
                UserName = user.UserName
            });
        }


        public async Task<IActionResult> Destroy(string id)
        {
            var user = await this.userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            await this.userManager.DeleteAsync(user);

            TempData["SuccesMessage"] = $"User {user.UserName} deleted.";
            return RedirectToAction(nameof(Index));
        }

        
        public async Task<IActionResult> AddToRole(string id)
        {
            var user = await this.userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var roles = await this.userManager.GetRolesAsync(user);

            var rolesSelectListItem = this.roleManager.Roles
                .Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Name
                })
                .ToList();

            return View(new UserWithRolesViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Roles = roles,
                AllRoles = rolesSelectListItem

            });
        }
        

        [HttpPost]
        public async Task<IActionResult> AddToRole(string id, string allroles)
        {
            var user = await this.userManager.FindByIdAsync(id);
            var roleExists = await this.roleManager.RoleExistsAsync(allroles);

            if (user == null || !roleExists)
            {
                return NotFound();
            }

            await this.userManager.AddToRoleAsync(user, allroles);

            TempData["SuccesMessage"] = $"User {user.UserName} added to {allroles} role. ";
            return RedirectToAction(nameof(Index));
        }

      //  [HttpPost]
      //  public async Task<IActionResult> AddToRole(AddUserToRoleFormModel model)
      //  {
      //      var roleExists = await this.roleManager.RoleExistsAsync(model.Role);
      //      var user = await this.userManager.FindByIdAsync(model.UserId);
      //      var userExists = user != null;
      //
      //      if (!roleExists || !userExists)
      //      {
      //          ModelState.AddModelError(String.Empty, "Invalid identity details");
      //      }
      //
      //      if(ModelState.IsValid)
      //      {
      //          return RedirectToAction(nameof(Index));
      //      }
      //
      //      await this.userManager.AddToRoleAsync(user, model.Role);
      //
      //      TempData.AddSuccessMessage($"User {user.UserName} added to {model.Role} role");
      //      return RedirectToAction(nameof(Index));
      //  }

        private void AddModelErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}