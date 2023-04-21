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
	public class CategoryOfTimbersService
	{
		private readonly ApplicationDbContext _context;
		public CategoryOfTimbersService(ApplicationDbContext context)
		{
			_context = context;
		}
		public IQueryable<CategoryOfTimber> GetCategoriesByName(string SearchString, IQueryable<CategoryOfTimber> categs)
		{
			categs = categs.Where(x => x.CategoryName.ToString().Contains(SearchString));
			return categs;
		}

		public IQueryable<CategoryOfTimber> GetCategories()
		{
			return from cats in _context.CategoryOfTimber
				   select cats;
		}
		public async Task CreateCategoryOfTimber(CategoryOfTimber categoryOfTimber)
		{
			_context.Add(categoryOfTimber);
			await _context.SaveChangesAsync();
		}
		public async Task<CategoryOfTimber> GetCategoryOfTimberDeatils(int? id)
		{
			return await _context.CategoryOfTimber
							.FirstOrDefaultAsync(m => m.Id == id);
		}

		public async Task<CategoryOfTimber> GetCategoryOfTimber(int? id)
		{
			return await _context.CategoryOfTimber.FindAsync(id);
		}

		public async Task UpdateCategoryOfTimber(CategoryOfTimber categoryOfTimber)
		{
			_context.Update(categoryOfTimber);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteCategoryOfTimber(int id)
		{
			var categoryOfTimber = await _context.CategoryOfTimber.FindAsync(id);
			if (categoryOfTimber != null)
			{
				_context.CategoryOfTimber.Remove(categoryOfTimber);
			}

			await _context.SaveChangesAsync();
		}

		public bool CategoryOfTimberExists(int id)
		{
			return _context.CategoryOfTimber.Any(e => e.Id == id);
		}
	}
}
