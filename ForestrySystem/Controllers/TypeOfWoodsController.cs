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
using System.Data;
using Microsoft.Data.SqlClient;
using ForestrySystem.Services;

namespace ForestrySystem.Controllers
{

	public class TypeOfWoodsController : Controller
	{
		private readonly TypeOfWoodsService _typeOfWoodService;

		public TypeOfWoodsController(TypeOfWoodsService typeOfWoodService)
		{
			_typeOfWoodService = typeOfWoodService;
		}


		// GET: TypeOfWoods
		public async Task<IActionResult> Index(string SearchString, string sortOrder)
		{
			ViewData["CurrentFilter"] = SearchString;
		
			return View(woods);
		}

		// GET: TypeOfWoods/Details/5
	
		public async Task<IActionResult> Details(int id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var typeOfWood = await _typeOfWoodService.GetTypeOfWoodById(id);
			if (typeOfWood == null)
			{
				return NotFound();
			}

			return View(typeOfWood);
		}

		// GET: TypeOfWoods/Create
		[Authorize(Roles = "Expert")]
		public IActionResult Create() => View();

		// POST: TypeOfWoods/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,SpeciesName,Origin,AmountForLogging,YearOfLogging")] TypeOfWood typeOfWood)
		{
			if (ModelState.IsValid)
			{
				//_context.Add(typeOfWood);
				//await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			// ako vsichko e minalo uspeshno vrushame true ako bugva false 
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
			//transfer to service
			//var typeOfWood = await _context.WoodTypes.FindAsync(id);
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
					//proverqvame dali go ima ako ima null vrushtame greshka 
					//_context.Update(typeOfWood);
					//await _context.SaveChangesAsync();
					//tuk izvikvame service
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
		public async Task<IActionResult> Delete(int id)
		{
			if (id == null || _context.WoodTypes == null)
			{
				return NotFound();
			}

			var typeOfWood = await _typeOfWoodService.GetTypeOfWoodById(id);
			
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
			//namirame po ID var typeOfWood = await _context.WoodTypes.FindAsync(id);
			if (typeOfWood != null)
			{
			//	_context.WoodTypes.Remove(typeOfWood);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}
	}
}
