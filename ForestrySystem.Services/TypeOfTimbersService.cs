using ForestrySystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForestrySystem.Services
{
	internal class TypeOfTimbersService
	{
		private ApplicationDbContext _context;

		public TypeOfTimbersService(ApplicationDbContext context)
		{
			_context = context;
		}

	}
}
