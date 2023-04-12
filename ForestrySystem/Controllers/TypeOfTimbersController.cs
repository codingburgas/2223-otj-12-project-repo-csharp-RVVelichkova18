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

	public class TypeOfTimbersController : Controller
	{
		private readonly ApplicationDbContext _context;


		public TypeOfTimbersController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: TypeOfTimbers
		public async Task<IActionResult> Index(string SearchString, string sortOrder)
		{
			ViewData["CurrentFilter"] = SearchString;
			var types = from t in _context.TypeOfTimber
						select t;
			if (!String.IsNullOrEmpty(SearchString))
			{
				types = types.Where(x => x.TimberName.ToString().Contains(SearchString));
			}
			return View(types);

			ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
			ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
			switch (sortOrder)
			{
				case "name_desc":
					types = types.OrderByDescending(s => s.TimberName);
					break;
				case "Date":
					types = types.OrderBy(s => s.YearOfLogging);
					break;
			}
			return View(await types.AsNoTracking().ToListAsync());
		}



		// GET: TypeOfTimbers/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.TypeOfTimber == null)
			{
				return NotFound();
			}

			var typeOfTimber = await _context.TypeOfTimber
				.FirstOrDefaultAsync(m => m.Id == id);
			if (typeOfTimber == null)
			{
				return NotFound();
			}

			return View(typeOfTimber);
		}

		// GET: TypeOfTimbers/Create
		[Authorize(Roles = "Expert")]
		public IActionResult Create()
		{
			return View();
		}

		// POST: TypeOfTimbers/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,TimberName,AmountForLogging,YearOfLogging")] TypeOfTimber typeOfTimber)
		{
			if (ModelState.IsValid)
			{
				_context.Add(typeOfTimber);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(typeOfTimber);
		}

		// GET: TypeOfTimbers/Edit/5
		[Authorize(Roles = "Expert")]
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.TypeOfTimber == null)
			{
				return NotFound();
			}

			var typeOfTimber = await _context.TypeOfTimber.FindAsync(id);
			if (typeOfTimber == null)
			{
				return NotFound();
			}
			return View(typeOfTimber);
		}

		// POST: TypeOfTimbers/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,TimberName,AmountForLogging,YearOfLogging")] TypeOfTimber typeOfTimber)
		{
			if (id != typeOfTimber.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(typeOfTimber);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!TypeOfTimberExists(typeOfTimber.Id))
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
			return View(typeOfTimber);
		}

		// GET: TypeOfTimbers/Delete/5
		[Authorize(Roles = "Expert")]
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.TypeOfTimber == null)
			{
				return NotFound();
			}

			var typeOfTimber = await _context.TypeOfTimber
				.FirstOrDefaultAsync(m => m.Id == id);
			if (typeOfTimber == null)
			{
				return NotFound();
			}

			return View(typeOfTimber);
		}

		// POST: TypeOfTimbers/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.TypeOfTimber == null)
			{
				return Problem("Entity set 'ApplicationDbContext.TypeOfTimber'  is null.");
			}
			var typeOfTimber = await _context.TypeOfTimber.FindAsync(id);
			if (typeOfTimber != null)
			{
				_context.TypeOfTimber.Remove(typeOfTimber);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool TypeOfTimberExists(int id)
		{
			return _context.TypeOfTimber.Any(e => e.Id == id);
		}
	}
}
