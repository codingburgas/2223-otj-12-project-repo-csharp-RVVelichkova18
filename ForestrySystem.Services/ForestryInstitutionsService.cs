using ForestrySystem.Data;
using ForestrySystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForestrySystem.Services
{
	public class ForestryInstitutionsService
	{
		private readonly ApplicationDbContext _context;
		public ForestryInstitutionsService(ApplicationDbContext context)
		{
			_context = context;
		}
		public IQueryable<ForestryInstitution> GetFilteredForestInstitutions(string SearchString, IQueryable<ForestryInstitution> institutions)
		{
			institutions = institutions.Where(x => x.Name.Contains(SearchString));
			return institutions;
		}

		public IQueryable<ForestryInstitution> GetForestInstitutions()
		{
			return from inst in _context.Institutions
				   select inst;
		}
		public async Task<ForestryInstitution> GetForestInstitutions(int? id)
		{
			return await _context.Institutions
							.FirstOrDefaultAsync(m => m.Id == id);
		}
		public async Task CreateForestInstitution(ForestryInstitution forestryInstitution)
		{
			_context.Add(forestryInstitution);
			await _context.SaveChangesAsync();
		}
		public async Task UpdateForestInstitution(ForestryInstitution forestryInstitution)
		{
			_context.Update(forestryInstitution);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteForestInstitution(int id)
		{
			var forestryInstitution = await _context.Institutions.FindAsync(id);
			if (forestryInstitution != null)
			{
				_context.Institutions.Remove(forestryInstitution);
			}

			await _context.SaveChangesAsync();
		}

		public bool ForestryInstitutionExists(int id)
		{
			return _context.Institutions.Any(e => e.Id == id);
		}
	}
}
