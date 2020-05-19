using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using school.Models;

namespace school.Controllers
{
    [Authorize(Roles="Admin")]
    public class StudentController : Controller
    {
        private readonly SchoolContext context;
        public StudentController(SchoolContext context)
        {
            this.context = context;
        }
        public async Task<IActionResult> Index(int? id)
        {
            HttpContext.Session.SetInt32("Class", id.Value);
            Class cls = await context.Class.SingleOrDefaultAsync(c => c.Id == id);
            if(cls != null)
            {
                HttpContext.Session.SetString("ClassString", cls.Name);
            }
            return View(await context.Student.Where(s=>s.ClassFk == id).ToListAsync());
        }
    }
}