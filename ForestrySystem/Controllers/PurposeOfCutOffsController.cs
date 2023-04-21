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
using ForestrySystem.Services;

namespace ForestrySystem.Controllers
{

	public class PurposeOfCutOffsController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly PurposeOfCutOffsService _purposeOfCutOffsService;

		public PurposeOfCutOffsController(ApplicationDbContext context, PurposeOfCutOffsService purposeOfCutOffsService )
		{
			_context = context;
			_purposeOfCutOffsService = purposeOfCutOffsService;

        }

		// GET: PurposeOfCutOffs
		public async Task<IActionResult> Index(string SearchString)
        {
            //return View(await _context.Institutions.ToListAsync());
            ViewData["CurrentFilter"] = SearchString;
            IQueryable<PurposeOfCutOff> purposes = _purposeOfCutOffsService.GetPurposeOfCutOffs();
            if (!String.IsNullOrEmpty(SearchString))
            {
                purposes = _purposeOfCutOffsService.GetFilteredPurposeOfCutOffs(SearchString, purposes);
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

            PurposeOfCutOff? purposeOfCutOff = await _purposeOfCutOffsService.GetPurposeOfCurOff(id);
            if (purposeOfCutOff == null)
            {
                return NotFound();
            }

            return View(purposeOfCutOff);
        }

        

        // GET: PurposeOfCutOffs/Create
        [Authorize(Roles = "Expert,Admin")]
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
                await _purposeOfCutOffsService.CreatePurposeOdCurOff(purposeOfCutOff);
                return RedirectToAction(nameof(Index));
            }
            return View(purposeOfCutOff);
		}

        

        // GET: PurposeOfCutOffs/Edit/5
        [Authorize(Roles = "Expert,Admin")]
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.PurposeOfCutOff == null)
			{
				return NotFound();
			}

			var purposeOfCutOff = await _purposeOfCutOffsService.GetPurposeOfCurOff(id);
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
                    await _purposeOfCutOffsService.UpdatePurposeOfCutOff(purposeOfCutOff);
                }
                catch (DbUpdateConcurrencyException)
				{
					if (!_purposeOfCutOffsService.PurposeOfCutOffExists(purposeOfCutOff.Id))
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
        [Authorize(Roles = "Expert,Admin")]
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.PurposeOfCutOff == null)
			{
				return NotFound();
			}

			var purposeOfCutOff = await _purposeOfCutOffsService.GetPurposeOfCurOff(id);
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
            await _purposeOfCutOffsService.RemovePurposeOfCutOff(id);
            return RedirectToAction(nameof(Index));
        }


	}
}
