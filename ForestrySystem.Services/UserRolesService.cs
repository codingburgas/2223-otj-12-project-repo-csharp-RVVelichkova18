using ForestrySystem.Data;
using ForestrySystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForestrySystem.Services
{
	public class UserRolesService
	{
		private ApplicationDbContext _context;
		private readonly UserManager<AppUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		//constructor dependency injection
		public UserRolesService(ApplicationDbContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			_roleManager = roleManager;
			_userManager = userManager;
			_context = context;
		}

		public async Task<List<AppUser>> GetUsersAsync()
		{
			var users = await _userManager.Users.ToListAsync();
			return users;
		}
		public async Task<List<string>> GetUserRolesAsync(AppUser user)
			=> new List<string>(await _userManager.GetRolesAsync(user));

	}
}
