using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ForestrySystem.Data;
using ForestrySystem.Models;
using ForestrySystem.Services;

namespace ForestrySystem.Controllers
{
	[Route("event")]
	public class EventsController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly EventsService _eventsService;
		public EventsController(ApplicationDbContext context, EventsService eventsService)
		{
			_context = context;
			_eventsService = eventsService;
		}

		// GET: Events
		public async Task<IActionResult> Index(string SearchString)
		{
			ViewData["CurrentFilter"] = SearchString;
			IQueryable<Events> events = _eventsService.GetEvents();
			if (!String.IsNullOrEmpty(SearchString))
			{
				events = _eventsService.GetFilteredEvents(SearchString, events);
			}
			return View(events);
		}

		

		[Route("findall")]
		public async Task<IActionResult> FindAllEvents()
		{
			var events = _context.Events.Select(events => new
			{
				id = events.Id,
				name = events.Name,
				date = events.Date.ToString("dd/MM/yyyy"),
				status = events.Status,
				purpose = events.Purpose,
				institution = events.Institutions,
			}).ToList();
			return new JsonResult(events);
		}

		// GET: Events/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.Events == null)
			{
				return NotFound();
			}

			Events? events = await _eventsService.GetEvent(id);
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
				await _eventsService.CreateEvent(events);
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

			var events = await _eventsService.GetEvent(id);
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
					await _eventsService.UpdateEvent(events);
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!_eventsService.EventsExists(events.Id))
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

			var events = _eventsService.GetEvent(id);
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
			await _eventsService.DeleteEvent(id);
			return RedirectToAction(nameof(Index));
		}


	}
}
