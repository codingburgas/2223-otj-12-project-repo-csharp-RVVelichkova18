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
using ForestrySystem.Services;
using ForestrySystem.Data.Models;

namespace ForestrySystem.Controllers
{

    public class ForestryInstitutionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ForestryInstitutionsService _forestryInstitutionServices;

        public ForestryInstitutionsController(ApplicationDbContext context, ForestryInstitutionsService forestryInstitutionService)
        {
            _context = context;
            _forestryInstitutionServices = forestryInstitutionService;
        }


        // GET: ForestryInstitutions
        public async Task<IActionResult> Index(string SearchString)
        {
            //return View(await _context.Institutions.ToListAsync());
            ViewData["CurrentFilter"] = SearchString;
            IQueryable<ForestryInstitution> institutions = _forestryInstitutionServices.GetForestInstitutions();
            if (!String.IsNullOrEmpty(SearchString))
            {
                institutions = _forestryInstitutionServices.GetFilteredForestInstitutions(SearchString, institutions);
            }
            return View(institutions);
        }



        // GET: ForestryInstitutions/Details/5

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Institutions == null)
            {
                return NotFound();
            }

            ForestryInstitution? forestryInstitution = await _forestryInstitutionServices.GetForestInstitutions(id);
            if (forestryInstitution == null)
            {
                return NotFound();
            }

            return View(forestryInstitution);
        }



        // GET: ForestryInstitutions/Create
        [Authorize(Roles = "Expert,Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: ForestryInstitutions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Expert,Admin")]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Location,TotalArea,GreenArea,UrbanizedArea,Email,Phone,Address")] ForestryInstitution forestryInstitution)
        {
            if (ModelState.IsValid)
            {
                await _forestryInstitutionServices.CreateForestInstitution(forestryInstitution);
                return RedirectToAction(nameof(Index));
            }
            return View(forestryInstitution);
        }



        // GET: ForestryInstitutions/Edit/5
        [Authorize(Roles = "Expert,Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Institutions == null)
            {
                return NotFound();
            }

            var forestryInstitution = await _forestryInstitutionServices.GetForestInstitutions(id);
            if (forestryInstitution == null)
            {
                return NotFound();
            }
            return View(forestryInstitution);
        }

        // POST: ForestryInstitutions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Expert,Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Location,TotalArea,GreenArea,UrbanizedArea,Email,Phone,Address")] ForestryInstitution forestryInstitution)
        {
            if (id != forestryInstitution.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _forestryInstitutionServices.UpdateForestInstitution(forestryInstitution);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_forestryInstitutionServices.ForestryInstitutionExists(forestryInstitution.Id))
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
            return View(forestryInstitution);
        }



        // GET: ForestryInstitutions/Delete/5
        [Authorize(Roles = "Expert,Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Institutions == null)
            {
                return NotFound();
            }

            var forestryInstitution = await _forestryInstitutionServices.GetForestInstitutions(id);
            if (forestryInstitution == null)
            {
                return NotFound();
            }

            return View(forestryInstitution);
        }

        // POST: ForestryInstitutions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Institutions == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Institutions'  is null.");
            }
            await _forestryInstitutionServices.DeleteForestInstitution(id);
            return RedirectToAction(nameof(Index));
        }



        public async Task<IActionResult> DisplayInstitution(int id)
        {
            DisplayInstitutionCategory temp = new DisplayInstitutionCategory();
            temp.institution = await _forestryInstitutionServices.GetForestInstitutions(id);

            return View(temp);
        }

        [HttpPost]
        public async Task<IActionResult> DisplayInstitution(DisplayInstitutionCategory category)
        {
            int year = int.Parse(category.Year);
            List<TypeOfTimber> templist = _context.TypeOfTimber.Where(i => i.YearOfLogging.Year == year).ToList();
            return View("DisplayTable", templist);
        }


    }
}
