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
	public class EventsService
	{
		private readonly ApplicationDbContext _context;
		public EventsService(ApplicationDbContext context)
		{
			_context = context;
		}
		public IQueryable<Events> GetEvents()
		{
			return from evs in _context.Events
				   select evs;
		}
		public async Task<Events> GetEvent(int? id)
		{
			return await _context.Events
							.FirstOrDefaultAsync(m => m.Id == id);
		}
		public async Task CreateEvent(Events events)
		{
			_context.Add(events);
			await _context.SaveChangesAsync();
		}
		public async Task UpdateEvent(Events events)
		{
			_context.Update(events);
			await _context.SaveChangesAsync();
		}
		public async Task DeleteEvent(int id)
		{
			var events = await _context.Events.FindAsync(id);
			if (events != null)
			{
				_context.Events.Remove(events);
			}

			await _context.SaveChangesAsync();
		}

		public bool EventsExists(int id)
		{
			return (_context.Events?.Any(e => e.Id == id)).GetValueOrDefault();
		}
		public IQueryable<Events> GetFilteredEvents(string SearchString, IQueryable<Events> events)
		{
			events = events.Where(x => x.Name.Contains(SearchString));
			return events;
		}
	}
}
