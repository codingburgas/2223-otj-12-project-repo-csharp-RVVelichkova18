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
using Microsoft.Data.SqlClient;

namespace ForestrySystem.Controllers
{

	public class TypeOfWoodsController : Controller
	{
		private readonly ApplicationDbContext _context;

		public TypeOfWoodsController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: TypeOfWoods
		public async Task<IActionResult> Index(string SearchString, string sortOrder)
		{
			ViewData["CurrentFilter"] = SearchString;
			var woods = from w in _context.WoodTypes
						select w;
			if (!String.IsNullOrEmpty(SearchString))
			{
				woods = woods.Where(x => x.Origin.ToString().Contains(SearchString));
			}
			return View(woods);

			ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
			ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
			switch (sortOrder)
			{
				case "name_desc":
					woods = woods.OrderByDescending(s => s.SpeciesName);
					break;
				case "Date":
					woods = woods.OrderBy(s => s.YearOfLogging);
					break;
			}
			return View(await woods.AsNoTracking().ToListAsync());
		}

		// GET: TypeOfWoods/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.WoodTypes == null)
			{
				return NotFound();
			}

			var typeOfWood = await _context.WoodTypes
				.FirstOrDefaultAsync(m => m.Id == id);
			if (typeOfWood == null)
			{
				return NotFound();
			}

			return View(typeOfWood);
		}

		// GET: TypeOfWoods/Create
		[Authorize(Roles = "Expert")]
		public IActionResult Create()
		{
			return View();
		}

		// POST: TypeOfWoods/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,SpeciesName,Origin,AmountForLogging,YearOfLogging")] TypeOfWood typeOfWood)
		{
			if (ModelState.IsValid)
			{
				_context.Add(typeOfWood);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(typeOfWood);
		}

		// GET: TypeOfWoods/Edit/5
		[Authorize(Roles = "Expert")]
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.WoodTypes == null)
			{
				return NotFound();
			}

			var typeOfWood = await _context.WoodTypes.FindAsync(id);
			if (typeOfWood == null)
			{
				return NotFound();
			}
			return View(typeOfWood);
		}

		// POST: TypeOfWoods/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,SpeciesName,Origin,AmountForLogging,YearOfLogging")] TypeOfWood typeOfWood)
		{
			if (id != typeOfWood.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(typeOfWood);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!TypeOfWoodExists(typeOfWood.Id))
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
			return View(typeOfWood);
		}

		// GET: TypeOfWoods/Delete/5
		[Authorize(Roles = "Expert")]
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.WoodTypes == null)
			{
				return NotFound();
			}

			var typeOfWood = await _context.WoodTypes
				.FirstOrDefaultAsync(m => m.Id == id);
			if (typeOfWood == null)
			{
				return NotFound();
			}

			return View(typeOfWood);
		}

		// POST: TypeOfWoods/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.WoodTypes == null)
			{
				return Problem("Entity set 'ApplicationDbContext.WoodTypes'  is null.");
			}
			var typeOfWood = await _context.WoodTypes.FindAsync(id);
			if (typeOfWood != null)
			{
				_context.WoodTypes.Remove(typeOfWood);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool TypeOfWoodExists(int id)
		{
			return _context.WoodTypes.Any(e => e.Id == id);
		}
	}
}
