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
	public class PurposeOfCutOffsService
	{
        private ApplicationDbContext _context;

        public PurposeOfCutOffsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<PurposeOfCutOff> GetFilteredPurposeOfCutOffs(string SearchString, IQueryable<PurposeOfCutOff> purposes)
        {
            purposes = purposes.Where(x => x.Purpose.ToString().Contains(SearchString));
            return purposes;
        }

        public IQueryable<PurposeOfCutOff> GetPurposeOfCutOffs()
        {
            return from purps in _context.PurposeOfCutOff
                   select purps;
        }
        public async Task<PurposeOfCutOff> GetPurposeOfCurOff(int? id)
        {
            return await _context.PurposeOfCutOff
                            .FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task CreatePurposeOdCurOff(PurposeOfCutOff purposeOfCutOff)
        {
            _context.Add(purposeOfCutOff);
            await _context.SaveChangesAsync();
        }
        public async Task UpdatePurposeOfCutOff(PurposeOfCutOff purposeOfCutOff)
        {
            _context.Update(purposeOfCutOff);
            await _context.SaveChangesAsync();
        }
        public async Task RemovePurposeOfCutOff(int id)
        {
            var purposeOfCutOff = await _context.PurposeOfCutOff.FindAsync(id);
            if (purposeOfCutOff != null)
            {
                _context.PurposeOfCutOff.Remove(purposeOfCutOff);
            }

            await _context.SaveChangesAsync();
        }

        public bool PurposeOfCutOffExists(int id)
        {
            return _context.PurposeOfCutOff.Any(e => e.Id == id);
        }
    }
}
