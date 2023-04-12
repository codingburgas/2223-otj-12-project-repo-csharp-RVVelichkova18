using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ForestrySystem.Data;
using ForestrySystem.Models;

namespace ForestrySystem.Controllers
{
	public class EventsController : Controller
	{
		private readonly ApplicationDbContext _context;

		public EventsController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: Events
		public async Task<IActionResult> Index(string SearchString)
		{
			ViewData["CurrentFilter"] = SearchString;
			var events = from evs in _context.Events
						 select evs;
			if (!String.IsNullOrEmpty(SearchString))
			{
				events = events.Where(x => x.Name.Contains(SearchString));
			}
			return View(events);
		}

		// GET: Events/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.Events == null)
			{
				return NotFound();
			}

			var events = await _context.Events
				.FirstOrDefaultAsync(m => m.Id == id);
			if (events == null)
			{
				return NotFound();
			}

			return View(events);
		}

		// GET: Events/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Events/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Name,Date,Status,Purpose,FIEventRefID")] Events events)
		{
			if (ModelState.IsValid)
			{
				_context.Add(events);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(events);
		}

		// GET: Events/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.Events == null)
			{
				return NotFound();
			}

			var events = await _context.Events.FindAsync(id);
			if (events == null)
			{
				return NotFound();
			}
			return View(events);
		}

		// POST: Events/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Date,Status,Purpose,FIEventRefID")] Events events)
		{
			if (id != events.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(events);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!EventsExists(events.Id))
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
			return View(events);
		}

		// GET: Events/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.Events == null)
			{
				return NotFound();
			}

			var events = await _context.Events
				.FirstOrDefaultAsync(m => m.Id == id);
			if (events == null)
			{
				return NotFound();
			}

			return View(events);
		}

		// POST: Events/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.Events == null)
			{
				return Problem("Entity set 'ApplicationDbContext.Events'  is null.");
			}
			var events = await _context.Events.FindAsync(id);
			if (events != null)
			{
				_context.Events.Remove(events);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool EventsExists(int id)
		{
			return (_context.Events?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}
