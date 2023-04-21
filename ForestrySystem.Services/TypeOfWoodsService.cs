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
	public class TypeOfWoodsService
	{
		private ApplicationDbContext _context;

		public TypeOfWoodsService(ApplicationDbContext context) 
		{ 
			_context = context;	
		}
		public async Task<TypeOfWood> GetTypeOfWoodById(int id) 
		{
			var typeOfWood = await _context.WoodTypes.FirstOrDefaultAsync(m => m.Id == id);
			return typeOfWood;
		}

		public async Task<IEnumerable<TypeOfWood>> GetTypeOfWoodByCriteria(string? criteria)
		{
			var woods = from w in _context.WoodTypes
						select w;
			if (!String.IsNullOrEmpty(criteria))
			{
				woods = woods.Where(x => x.Origin.ToString().Contains(criteria));
			}
			return woods;
		}
        public async Task UpdateTypeOfWood(TypeOfWood typeOfWood)
        {
            _context.Update(typeOfWood);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteTypeOfWood(int id)
        {
            var typeOfWood = await _context.WoodTypes.FindAsync(id);
            if (typeOfWood != null)
            {
                _context.WoodTypes.Remove(typeOfWood);
            }

            await _context.SaveChangesAsync();
        }

		public bool TypeOfWoodExists(int id)
		{
			return _context.WoodTypes.Any(e => e.Id == id);
        }
    }
}
