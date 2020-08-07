using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using school.Models;

namespace school.Controllers
{
    [Authorize(Roles = "Admin")]
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
                HttpContext.Session.SetString("StageString", stage.NameStage);
            }
            if (id == null)
            {
                return View("Index", "Home");
            }
            return View(await context.Class.Where(c => c.Stage == id).ToListAsync());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name")]Class classStudent)
        {
            if (ModelState.IsValid)
            {
                classStudent.Stage = HttpContext.Session.GetInt32("Stage").Value;
                await context.Class.AddAsync(classStudent);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { id = classStudent.Stage });
            }
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
            if (cls.Id != id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                context.Update(cls);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { id = cls.Stage });
            }
            ViewData["Stage"] = new SelectList(context.Stage, "StageId", "NameStage", cls.Stage);
            return View(cls);
        }

        [HttpGet]
        public async Task<IActionResult> ListClass()
        {
            var stage = HttpContext.Session.GetInt32("Stage");
            ViewData["stage"] = stage;
            return View(await context.Class.Where(c => c.Stage == stage).ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var cls = await context.Class.SingleOrDefaultAsync(c => c.Id == id);
            var absents = context.Absent.Where(c => c.StudentNavigation.ClassFk == id);
            var students = context.Student.Where(s => s.ClassFk == id);
            if(cls != null) {
                context.RemoveRange(cls);
                context.RemoveRange(absents);
                context.RemoveRange(students);
                await context.SaveChangesAsync();
            }
            else
            {
                return Unauthorized();
            }
            return RedirectToAction(nameof(ListClass));
        }

    }
}