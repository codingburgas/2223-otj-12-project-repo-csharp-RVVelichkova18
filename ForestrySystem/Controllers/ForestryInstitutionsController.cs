using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ForestrySystem.Data;
using ForestrySystem.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace ForestrySystem.Controllers
{

	public class ForestryInstitutionsController : Controller
	{
		private readonly ApplicationDbContext _context;

		public ForestryInstitutionsController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: ForestryInstitutions
		public async Task<IActionResult> Index(string SearchString)
		{
			//return View(await _context.Institutions.ToListAsync());
			ViewData["CurrentFilter"] = SearchString;
			IQueryable<ForestryInstitution> institutions = GetForestIndustries();
			if (!String.IsNullOrEmpty(SearchString))
			{
				institutions = GetFilteredForestIndustries(SearchString, institutions);
			}
			return View(institutions);
		}

		private static IQueryable<ForestryInstitution> GetFilteredForestIndustries(string SearchString, IQueryable<ForestryInstitution> institutions)
		{
			institutions = institutions.Where(x => x.Name.Contains(SearchString));
			return institutions;
		}

		private IQueryable<ForestryInstitution> GetForestIndustries()
		{
			return from inst in _context.Institutions
				   select inst;
		}

		// GET: ForestryInstitutions/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.Institutions == null)
			{
				return NotFound();
			}

			ForestryInstitution? forestryInstitution = await GetForestIndustry(id);
			if (forestryInstitution == null)
			{
				return NotFound();
			}

			return View(forestryInstitution);
		}

		private async Task<ForestryInstitution> GetForestIndustry(int? id)
		{
			return await _context.Institutions
							.FirstOrDefaultAsync(m => m.Id == id);
		}

		// GET: ForestryInstitutions/Create
		[Authorize(Roles = "Expert,Admin")]
		public IActionResult Create()
		{
			return View();
		}

		// POST: ForestryInstitutions/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Name,Description,Location,TotalArea,GreenArea,UrbanizedArea,Email,Phone,Address")] ForestryInstitution forestryInstitution)
		{
			if (ModelState.IsValid)
			{
				await CreateForestIndustry(forestryInstitution);
				return RedirectToAction(nameof(Index));
			}
			return View(forestryInstitution);
		}

		private async Task CreateForestIndustry(ForestryInstitution forestryInstitution)
		{
			_context.Add(forestryInstitution);
			await _context.SaveChangesAsync();
		}

		// GET: ForestryInstitutions/Edit/5
		[Authorize(Roles = "Expert,Admin")]
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.Institutions == null)
			{
				return NotFound();
			}

			var forestryInstitution = await GetForestIndustry(id);
			if (forestryInstitution == null)
			{
				return NotFound();
			}
			return View(forestryInstitution);
		}

		// POST: ForestryInstitutions/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Location,TotalArea,GreenArea,UrbanizedArea,Email,Phone,Address")] ForestryInstitution forestryInstitution)
		{
			if (id != forestryInstitution.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					await UpdateForestIndustry(forestryInstitution);
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!ForestryInstitutionExists(forestryInstitution.Id))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}
			return View(forestryInstitution);
		}

		private async Task UpdateForestIndustry(ForestryInstitution forestryInstitution)
		{
			_context.Update(forestryInstitution);
			await _context.SaveChangesAsync();
		}

		// GET: ForestryInstitutions/Delete/5
		[Authorize(Roles = "Expert,Admin")]
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.Institutions == null)
			{
				return NotFound();
			}

			var forestryInstitution = await GetForestIndustry(id);
			if (forestryInstitution == null)
			{
				return NotFound();
			}

			return View(forestryInstitution);
		}

		// POST: ForestryInstitutions/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.Institutions == null)
			{
				return Problem("Entity set 'ApplicationDbContext.Institutions'  is null.");
			}
			await DeleteForestIndustry(id);
			return RedirectToAction(nameof(Index));
		}

		private async Task DeleteForestIndustry(int id)
		{
			var forestryInstitution = await _context.Institutions.FindAsync(id);
			if (forestryInstitution != null)
			{
				_context.Institutions.Remove(forestryInstitution);
			}

			await _context.SaveChangesAsync();
		}

		private bool ForestryInstitutionExists(int id)
		{
			return _context.Institutions.Any(e => e.Id == id);
		}

		public IActionResult DisplayInstitution(int id)
		{
			return View(_context.Institutions.Where(e => e.Id == id).First());
		}
	}
}
