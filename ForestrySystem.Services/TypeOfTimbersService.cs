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
	public class TypeOfTimbersService
	{
		private ApplicationDbContext _context;

		public TypeOfTimbersService(ApplicationDbContext context)
		{
			_context = context;
		}

        public IQueryable<TypeOfTimber> GetFilteredTypeOfTimbers(string SearchString, IQueryable<TypeOfTimber> types)
        {
            types = types.Where(x => x.TimberName.ToString().Contains(SearchString));
            return types;
        }

        public IQueryable<TypeOfTimber> GetTypeOfTimbers()
        {
            return from t in _context.TypeOfTimber
                   select t;
        }
        public async Task<TypeOfTimber> GetTypeOfTimber(int? id)
        {
            return await _context.TypeOfTimber
                            .FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task CreateTypeOfTimber(TypeOfTimber typeOfTimber)
        {
            _context.Add(typeOfTimber);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateTypeOfTimber(TypeOfTimber typeOfTimber)
        {
            _context.Update(typeOfTimber);
            await _context.SaveChangesAsync();
        }
        public async Task RemoveTypeOfTimber(int id)
        {
            var typeOfTimber = await _context.TypeOfTimber.FindAsync(id);
            if (typeOfTimber != null)
            {
                _context.TypeOfTimber.Remove(typeOfTimber);
            }

            await _context.SaveChangesAsync();
        }

        public bool TypeOfTimberExists(int id)
        {
            return _context.TypeOfTimber.Any(e => e.Id == id);
        }
    }
}
