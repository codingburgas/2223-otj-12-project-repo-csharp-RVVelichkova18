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
		public IQueryable<ForestryInstitution> GetFilteredForestIndustries(string SearchString, IQueryable<ForestryInstitution> institutions)
		{
			institutions = institutions.Where(x => x.Name.Contains(SearchString));
			return institutions;
		}

		public IQueryable<ForestryInstitution> GetForestIndustries()
		{
			return from inst in _context.Institutions
				   select inst;
		}
		public async Task<ForestryInstitution> GetForestIndustry(int? id)
		{
			return await _context.Institutions
							.FirstOrDefaultAsync(m => m.Id == id);
		}
		public async Task CreateForestIndustry(ForestryInstitution forestryInstitution)
		{
			_context.Add(forestryInstitution);
			await _context.SaveChangesAsync();
		}
		public async Task UpdateForestIndustry(ForestryInstitution forestryInstitution)
		{
			_context.Update(forestryInstitution);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteForestIndustry(int id)
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
