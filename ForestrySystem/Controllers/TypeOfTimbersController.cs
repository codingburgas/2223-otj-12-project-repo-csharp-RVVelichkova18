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
using ForestrySystem.Services;

namespace ForestrySystem.Controllers
{

	public class TypeOfTimbersController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly TypeOfTimbersService _typeOfTimbersService;


		public TypeOfTimbersController(ApplicationDbContext context, TypeOfTimbersService typeOfTimbersService)
		{
			_context = context;
			_typeOfTimbersService = typeOfTimbersService;
		}

		// GET: TypeOfTimbers
		public async Task<IActionResult> Index(string SearchString, string sortOrder)
        {
            ViewData["CurrentFilter"] = SearchString;
            IQueryable<TypeOfTimber> types = _typeOfTimbersService.GetTypeOfTimbers();
            if (!String.IsNullOrEmpty(SearchString))
            {
                types = _typeOfTimbersService.GetFilteredTypeOfTimbers(SearchString, types);
            }
            return View(types);
        }




        // GET: TypeOfTimbers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TypeOfTimber == null)
            {
                return NotFound();
            }

            TypeOfTimber? typeOfTimber = await _typeOfTimbersService.GetTypeOfTimber(id);
            if (typeOfTimber == null)
            {
                return NotFound();
            }

            return View(typeOfTimber);
        }

        

        // GET: TypeOfTimbers/Create
        [Authorize(Roles = "Expert,Admin")]
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
                await _typeOfTimbersService.CreateTypeOfTimber(typeOfTimber);
                return RedirectToAction(nameof(Index));
            }
            return View(typeOfTimber);
		}

        

        // GET: TypeOfTimbers/Edit/5
        [Authorize(Roles = "Expert,Admin")]
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.TypeOfTimber == null)
			{
				return NotFound();
			}

			var typeOfTimber = await _typeOfTimbersService.GetTypeOfTimber(id);
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
                    await _typeOfTimbersService.UpdateTypeOfTimber(typeOfTimber);
                }
                catch (DbUpdateConcurrencyException)
				{
					if (!_typeOfTimbersService.TypeOfTimberExists(typeOfTimber.Id))
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
        [Authorize(Roles = "Expert,Admin")]
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.TypeOfTimber == null)
			{
				return NotFound();
			}

			var typeOfTimber = await _typeOfTimbersService.GetTypeOfTimber(id);
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
            await _typeOfTimbersService.RemoveTypeOfTimber(id);
            return RedirectToAction(nameof(Index));
        }

        
	}
}
