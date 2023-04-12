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

namespace ForestrySystem.Controllers
{

	public class CategoryOfTimbersController : Controller
	{
		private readonly ApplicationDbContext _context;

		public CategoryOfTimbersController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: CategoryOfTimbers
		public async Task<IActionResult> Index(string SearchString)
		{
			//return View(await _context.CategoryOfTimber.ToListAsync());
			ViewData["CurrentFilter"] = SearchString;
			var categs = from cats in _context.CategoryOfTimber
						 select cats;

			if (!String.IsNullOrEmpty(SearchString))
			{
				categs = categs.Where(x => x.CategoryName.ToString().Contains(SearchString));
			}
			return View(categs);
		}

		// GET: CategoryOfTimbers/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.CategoryOfTimber == null)
			{
				return NotFound();
			}

			var categoryOfTimber = await _context.CategoryOfTimber
				.FirstOrDefaultAsync(m => m.Id == id);
			if (categoryOfTimber == null)
			{
				return NotFound();
			}

			return View(categoryOfTimber);
		}

		// GET: CategoryOfTimbers/Create
		[Authorize(Roles = "Expert")]
		public IActionResult Create()
		{
			return View();
		}

		// POST: CategoryOfTimbers/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "Expert")]
		public async Task<IActionResult> Create([Bind("Id,CategoryName,AmountForLogging,YearOfLogging")] CategoryOfTimber categoryOfTimber)
		{
			if (ModelState.IsValid)
			{
				_context.Add(categoryOfTimber);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(categoryOfTimber);
		}

		// GET: CategoryOfTimbers/Edit/5
		[Authorize(Roles = "Expert")]
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.CategoryOfTimber == null)
			{
				return NotFound();
			}

			var categoryOfTimber = await _context.CategoryOfTimber.FindAsync(id);
			if (categoryOfTimber == null)
			{
				return NotFound();
			}
			return View(categoryOfTimber);
		}

		// POST: CategoryOfTimbers/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "Expert")]
		public async Task<IActionResult> Edit(int id, [Bind("Id,CategoryName,AmountForLogging,YearOfLogging")] CategoryOfTimber categoryOfTimber)
		{
			if (id != categoryOfTimber.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(categoryOfTimber);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!CategoryOfTimberExists(categoryOfTimber.Id))
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
			return View(categoryOfTimber);
		}

		// GET: CategoryOfTimbers/Delete/5
		[Authorize(Roles = "Expert")]
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.CategoryOfTimber == null)
			{
				return NotFound();
			}

			var categoryOfTimber = await _context.CategoryOfTimber
				.FirstOrDefaultAsync(m => m.Id == id);
			if (categoryOfTimber == null)
			{
				return NotFound();
			}

			return View(categoryOfTimber);
		}

		// POST: CategoryOfTimbers/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.CategoryOfTimber == null)
			{
				return Problem("Entity set 'ApplicationDbContext.CategoryOfTimber'  is null.");
			}
			var categoryOfTimber = await _context.CategoryOfTimber.FindAsync(id);
			if (categoryOfTimber != null)
			{
				_context.CategoryOfTimber.Remove(categoryOfTimber);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool CategoryOfTimberExists(int id)
		{
			return _context.CategoryOfTimber.Any(e => e.Id == id);
		}
	}
}
