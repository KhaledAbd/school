using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore;
using school.Models;

namespace school.Controllers
{
    [Authorize(Roles ="Admin")]
    public class ClassController : Controller
    {
        private readonly SchoolContext context;
        public ClassController(SchoolContext context)
        {
            this.context = context;
        }
        public async Task<IActionResult> Index(int? id)
        {
            HttpContext.Session.SetInt32("Stage", id.Value);

            Stage stage = await context.Stage.SingleOrDefaultAsync(n => n.StageId == id.Value);
            if (stage != null) {
                HttpContext.Session.SetString("StageString",stage.NameStage);
            }
            if (id == null)
            {
                return View("Index", "Home");
            }
            return View(await context.Class.Where(c=>c.Id == id).ToListAsync());
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Stage"] = new SelectList(context.Stage, "StageId", "NameStage");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name,Stage")]Class classStudent)
        {
            if (ModelState.IsValid)
            {
                await context.Class.AddAsync(classStudent);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { id = classStudent.Stage});
            }
            ViewData["Stage"] = new SelectList(context.Stage, "StageId", "NameStage", classStudent.StageNavigation);

            return View(classStudent);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Class cls = await context.Class.FirstOrDefaultAsync(i => i.Id == id);
            ViewData["Stage"] = new SelectList(context.Stage, "StageId", "NameStage", cls.Stage);
            return View(cls);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Class cls, int id)
        {
            if(cls.Id != id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                context.Update(cls);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), cls.Id);
            }
            ViewData["Stage"] = new SelectList(context.Stage, "StageId", "NameStage", cls.Stage);
            return View(cls);
        }
    }
}