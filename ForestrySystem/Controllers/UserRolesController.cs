using ForestrySystem.Data;
using ForestrySystem.Models;
using ForestrySystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ForestrySystem.Controllers
{
	[Authorize(Roles = "Admin")]
	public class UserRolesController : Controller
	{
		private ApplicationDbContext _context;
		private readonly UserManager<AppUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly UserRolesService _userRolesService;
		//constructor dependency injection
		public UserRolesController(ApplicationDbContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			_roleManager = roleManager;
			_userManager = userManager;
			_context = context;
		}

		public UserRolesController(UserRolesService userRolesService)
		{
			_userRolesService = userRolesService;
		}
		public async Task<IActionResult> Index()
        {
            List<UserRolesViewModel> userRolesViewModel = await _userRolesService.GetUsers();
            return View(userRolesViewModel);
        }


        public async Task<IActionResult> Manage(string userId)
        {
            ViewBag.userId = userId;
            AppUser user = await _userRolesService.GetUser(userId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("NotFound");
            }
            ViewBag.UserName = user.UserName;
            List<ManageUserRolesViewModel> model = await _userRolesService.GenerateViewModel(user);
            return View(model);
        }

        [HttpPost]
		public async Task<IActionResult> Manage(List<ManageUserRolesViewModel> model, string userId)
        {
            var user = await _userRolesService.GetUser(userId);
            if (user == null)
            {
                return View();
            }
            IdentityResult result = await _userRolesService.RemoveRole(user);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user existing roles");
                return View(model);
            }
            result = await _userManager.AddToRolesAsync(user, model.Where(x => x.Selected).Select(y => y.RoleName));
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected roles to user");
                return View(model);
            }
            return RedirectToAction("Index");
        }

        

        public async Task<IActionResult> Delete(List<ManageUserRolesViewModel> model, string Id)
		{
			var UserToDelete = await _userRolesService.GetUser(Id);

			if (UserToDelete != null)
            {
                IdentityResult result = await _userRolesService.DeleteUser(UserToDelete);
                if (result.Succeeded)
                {
                    return View(model);
                }
            }
            return RedirectToAction("Index");
		}

    }
}