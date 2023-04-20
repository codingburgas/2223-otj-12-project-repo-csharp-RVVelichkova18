﻿using ForestrySystem.Models;
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
		private readonly UserManager<AppUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		public UserRolesController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			_roleManager = roleManager;
			_userManager = userManager;
		}
		public async Task<IActionResult> Index()
		{
			//var users = await _userManager.Users.ToListAsync();
			//var userRolesViewModel = new List<UserRolesViewModel>();
			//foreach (AppUser user in users)
			//{
			//	var thisViewModel = new UserRolesViewModel();
			//	thisViewModel.UserId = user.Id;
			//	thisViewModel.Email = user.Email;
			//	thisViewModel.FirstName = user.firstName;
			//	thisViewModel.LastName = user.lastName;
			//	thisViewModel.Roles = await GetUserRoles(user);
			//	userRolesViewModel.Add(thisViewModel);
			//}
			return View(userRolesViewModel);
		}
		private async Task<List<string>> GetUserRoles(AppUser user)
			=> new List<string>(await _userManager.GetRolesAsync(user));
		public async Task<IActionResult> Manage(string userId)
		{
			ViewBag.userId = userId;
			//var user = await _userManager.FindByIdAsync(userId);
			if (user == null)
			{
				ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
				return View("NotFound");
			}
			//ViewBag.UserName = user.UserName;
			//var model = new List<ManageUserRolesViewModel>();
			//foreach (var role in _roleManager.Roles)
			//{
			//	var userRolesViewModel = new ManageUserRolesViewModel
			//	{
			//		RoleId = role.Id,
			//		RoleName = role.Name
			//	};
			//	if (await _userManager.IsInRoleAsync(user, role.Name))
			//	{
			//		userRolesViewModel.Selected = true;
			//	}
			//	else
			//	{
			//		userRolesViewModel.Selected = false;
			//	}
			//	model.Add(userRolesViewModel);
			//}
			return View(model);
		}
		[HttpPost]
		public async Task<IActionResult> Manage(List<ManageUserRolesViewModel> model, string userId)
		{
			//var user = await _userManager.FindByIdAsync(userId);
			//if (user == null)
			//{
			//	return View();
			//}
			//var roles = await _userManager.GetRolesAsync(user);
			//var result = await _userManager.RemoveFromRolesAsync(user, roles);
			//if (!result.Succeeded)
			//{
			//	ModelState.AddModelError("", "Cannot remove user existing roles");
			//	return View(model);
			//}
			//result = await _userManager.AddToRolesAsync(user, model.Where(x => x.Selected).Select(y => y.RoleName));
			//if (!result.Succeeded)
			//{
			//	ModelState.AddModelError("", "Cannot add selected roles to user");
			//	return View(model);
			//}
			return RedirectToAction("Index");
		}


		public async Task<IActionResult> Delete(List<ManageUserRolesViewModel> model, string Id)
		{
			//var UserToDelete = await _userManager.FindByIdAsync(Id);

			if (UserToDelete != null)
			{
				IdentityResult result = await _userManager.DeleteAsync(UserToDelete);
			if (result.Succeeded)
			{
					return View(model);
			}
			}
			return RedirectToAction("Index");
		}


	}
}
