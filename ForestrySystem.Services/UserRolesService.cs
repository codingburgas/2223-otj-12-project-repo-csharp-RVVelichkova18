using ForestrySystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForestrySystem.Services
{
	internal class UserRolesService
	{
		private ApplicationDbContext _context;

		public UserRolesService(ApplicationDbContext context)
		{
			_context = context;
		}

	}
}
