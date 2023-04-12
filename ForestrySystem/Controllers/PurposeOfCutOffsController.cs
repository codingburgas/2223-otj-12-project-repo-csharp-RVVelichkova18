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

	public class PurposeOfCutOffsController : Controller
	{
		private readonly ApplicationDbContext _context;

		public PurposeOfCutOffsController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: PurposeOfCutOffs
		public async Task<IActionResult> Index(string SearchString)
		{
			//return View(await _context.Institutions.ToListAsync());
			ViewData["CurrentFilter"] = SearchString;
			var purposes = from purps in _context.PurposeOfCutOff
						   select purps;
			if (!String.IsNullOrEmpty(SearchString))
			{
				purposes = purposes.Where(x => x.Purpose.ToString().Contains(SearchString));
			}
			return View(purposes);
		}

		// GET: PurposeOfCutOffs/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.PurposeOfCutOff == null)
			{
				return NotFound();
			}

			var purposeOfCutOff = await _context.PurposeOfCutOff
				.FirstOrDefaultAsync(m => m.Id == id);
			if (purposeOfCutOff == null)
			{
				return NotFound();
			}

			return View(purposeOfCutOff);
		}

		// GET: PurposeOfCutOffs/Create
		[Authorize(Roles = "Expert")]
		public IActionResult Create()
		{
			return View();
		}

		// POST: PurposeOfCutOffs/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Purpose,PercentagePerYear")] PurposeOfCutOff purposeOfCutOff)
		{
			if (ModelState.IsValid)
			{
				_context.Add(purposeOfCutOff);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(purposeOfCutOff);
		}

		// GET: PurposeOfCutOffs/Edit/5
		[Authorize(Roles = "Expert")]
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.PurposeOfCutOff == null)
			{
				return NotFound();
			}

			var purposeOfCutOff = await _context.PurposeOfCutOff.FindAsync(id);
			if (purposeOfCutOff == null)
			{
				return NotFound();
			}
			return View(purposeOfCutOff);
		}

		// POST: PurposeOfCutOffs/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Purpose,PercentagePerYear")] PurposeOfCutOff purposeOfCutOff)
		{
			if (id != purposeOfCutOff.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(purposeOfCutOff);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!PurposeOfCutOffExists(purposeOfCutOff.Id))
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
			return View(purposeOfCutOff);
		}

		// GET: PurposeOfCutOffs/Delete/5
		[Authorize(Roles = "Expert")]
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.PurposeOfCutOff == null)
			{
				return NotFound();
			}

			var purposeOfCutOff = await _context.PurposeOfCutOff
				.FirstOrDefaultAsync(m => m.Id == id);
			if (purposeOfCutOff == null)
			{
				return NotFound();
			}

			return View(purposeOfCutOff);
		}

		// POST: PurposeOfCutOffs/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.PurposeOfCutOff == null)
			{
				return Problem("Entity set 'ApplicationDbContext.PurposeOfCutOff'  is null.");
			}
			var purposeOfCutOff = await _context.PurposeOfCutOff.FindAsync(id);
			if (purposeOfCutOff != null)
			{
				_context.PurposeOfCutOff.Remove(purposeOfCutOff);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool PurposeOfCutOffExists(int id)
		{
			return _context.PurposeOfCutOff.Any(e => e.Id == id);
		}
	}
}
