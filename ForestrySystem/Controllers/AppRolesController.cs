﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ForestrySystem.Controllers
{
	[Authorize(Roles = "Admin")]
	public class AppRolesController : Controller
	{
		private readonly RoleManager<IdentityRole> _roleManager;
		public AppRolesController(RoleManager<IdentityRole> roleManager)
		{
			_roleManager = roleManager;
		}

		public IActionResult Index()
		{
			var roles = _roleManager.Roles;
			return View(roles);
		}
		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(IdentityRole model)
		{
			//No Dupes
			if (!_roleManager.RoleExistsAsync(model.Name).GetAwaiter().GetResult())
			{
				_roleManager.CreateAsync(new IdentityRole(model.Name)).GetAwaiter().GetResult();
			}
			return RedirectToAction("Index");
		}



	}
}
