﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerSite.Data;
using ServerSite.Models;
using SharedVm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize("Bearer")]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<CategoryVm>>> Get()
        {
            return await _context.Categories
                .Select(x => new CategoryVm { Name = x.Name })
                .ToListAsync();
        }

        [HttpGet("{id}")]
        //[Authorize(Roles ="admin")]
        public async Task<ActionResult<CategoryVm>> GetId(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            var categoryVm = new CategoryVm
            {
                Id = category.Id,
                Name = category.Name
            };

            return categoryVm;
        }

        [HttpPut("{id}")]
        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> Put(int id, CategoryVm categoryVm)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            category.Name = categoryVm.Name;
            await _context.SaveChangesAsync();

            return Accepted();
        }

        [HttpPost]
        //[Authorize(Roles = "admin")]
        public async Task<ActionResult<CategoryVm>> Post(CategoryVm categoryVm)
        {
            var category = new Category
            {
                Name = categoryVm.Name
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCR", new { id = category.Id }, new BrandVm { Id = category.Id, Name = category.Name });
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return Accepted();
        }

    }
}