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
        public async Task<List<UserRolesViewModel>> GetUsers()
        {
            var users = await GetUsersAsync();
            var userRolesViewModel = new List<UserRolesViewModel>();
            foreach (AppUser user in users)
            {
                var thisViewModel = new UserRolesViewModel();
                thisViewModel.UserId = user.Id;
                thisViewModel.Email = user.Email;
                thisViewModel.FirstName = user.firstName;
                thisViewModel.LastName = user.lastName;
                thisViewModel.Roles = await GetUserRolesAsync(user);
                userRolesViewModel.Add(thisViewModel);
            }

            return userRolesViewModel;
        }

        public async Task<List<ManageUserRolesViewModel>> GenerateViewModel(AppUser user)
        {
            var model = new List<ManageUserRolesViewModel>();
            foreach (var role in _roleManager.Roles)
            {
                var userRolesViewModel = new ManageUserRolesViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRolesViewModel.Selected = true;
                }
                else
                {
                    userRolesViewModel.Selected = false;
                }
                model.Add(userRolesViewModel);
            }

            return model;
        }

        public async Task<AppUser> GetUser(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }
        public async Task<IdentityResult> RemoveRole(AppUser user)
        {
            var roles = await GetUserRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);
            return result;
        }
        public Task<IdentityResult> DeleteUser(AppUser UserToDelete)
        {
            return _userManager.DeleteAsync(UserToDelete);
        }
    }
}
