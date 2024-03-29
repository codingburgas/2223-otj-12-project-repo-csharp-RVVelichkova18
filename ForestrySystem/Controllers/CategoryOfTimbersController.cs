﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ForestrySystem.Data;
using ForestrySystem.Models;
using Microsoft.AspNetCore.Authorization;
using ForestrySystem.Services;

namespace ForestrySystem.Controllers
{

	public class CategoryOfTimbersController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly CategoryOfTimbersService _categoryOfTimberService;

		public CategoryOfTimbersController(ApplicationDbContext context, CategoryOfTimbersService categoryOfTimberService)
		{
			_context = context;
			_categoryOfTimberService = categoryOfTimberService;
		}

		// GET: CategoryOfTimbers
		public async Task<IActionResult> Index(string SearchString)
		{
			//return View(await _context.CategoryOfTimber.ToListAsync());
			ViewData["CurrentFilter"] = SearchString;
			IQueryable<CategoryOfTimber> categs = _categoryOfTimberService.GetCategories();

			if (!String.IsNullOrEmpty(SearchString))
			{
				categs = _categoryOfTimberService.GetCategoriesByName(SearchString, categs);
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

			CategoryOfTimber? categoryOfTimber = await _categoryOfTimberService.GetCategoryOfTimberDeatils(id);
			if (categoryOfTimber == null)
			{
				return NotFound();
			}

			return View(categoryOfTimber);
		}

		

		// GET: CategoryOfTimbers/Create
		[Authorize(Roles = "Expert,Admin")]
		public IActionResult Create()
		{
			return View();
		}

		// POST: CategoryOfTimbers/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "Expert")]
		public async Task<IActionResult> Create([Bind("Id,CategoryName,AmountForLogging,YearOfLogging")] CategoryOfTimber categoryOfTimber)
		{
			if (ModelState.IsValid)
			{
				await _categoryOfTimberService.CreateCategoryOfTimber(categoryOfTimber);
				return RedirectToAction(nameof(Index));
			}
			return View(categoryOfTimber);
		}

		

		// GET: CategoryOfTimbers/Edit/5
		[Authorize(Roles = "Expert,Admin")]
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.CategoryOfTimber == null)
			{
				return NotFound();
			}

			CategoryOfTimber? categoryOfTimber = await _categoryOfTimberService.GetCategoryOfTimber(id);
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
		[Authorize(Roles = "Expert,Admin")]
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
					await _categoryOfTimberService.UpdateCategoryOfTimber(categoryOfTimber);
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!_categoryOfTimberService.CategoryOfTimberExists(categoryOfTimber.Id))
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
		[Authorize(Roles = "Expert,Admin")]
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.CategoryOfTimber == null)
			{
				return NotFound();
			}

			var categoryOfTimber = _categoryOfTimberService.GetCategoryOfTimber(id);
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
			await _categoryOfTimberService.DeleteCategoryOfTimber(id);
			return RedirectToAction(nameof(Index));
		}
	}
}
