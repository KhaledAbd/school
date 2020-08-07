using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using school.Models;

namespace school.Controllers
{
    [Route("api/absent")]
    [ApiController]
    public class AbsentApiController : ControllerBase
    {
        private readonly SchoolContext context;
        public AbsentApiController(SchoolContext context)
        {
            this.context = context;
        }

        // /api/absent/setAbsent
        [HttpPost]
        [Route("setAbsent")]
        public async Task<IActionResult> GetAbsent(IEnumerable<Absent> absents)
        {
            var isInsert = false;
            if (!context.Absent.Any(a => a.DateAbsent.Date == DateTime.Today && a.StudentId == absents.First().StudentId))
            {
                await context.AddRangeAsync(absents);
                SaveStudentAbsent(absents);
                await context.SaveChangesAsync();
                isInsert = true;
            }
            return Ok(new { isInsert = isInsert });
        }

        private void SaveStudentAbsent(IEnumerable<Absent> absents)
        {
            foreach(var absent in absents)
            {
                var student = context.Student.SingleOrDefault(a => a.StudentId == absent.StudentId);
                if(student != null) { 
                    if (absent.AbsentCheck.Value) {
                        student.TimeAbsent++;
                        student.TimeAbsentDaily++;
                    }
                    else
                    {
                        student.TimeAbsentDaily = 0;
                    }
                }
            }
        }
        // /api/absent/setStudent
        [HttpPost]
        [Route("setStudent")]
        public async Task<IActionResult> AddSudent(Student student)
        {
            if (student == null)
                return Unauthorized();
            int? Cls;
            if (HttpContext.Session.GetInt32("Stage") != null){
                if((Cls = HttpContext.Session.GetInt32("Class")) != null){
                    student.TimeAbsent = 0;
                }
            }
            await context.AddAsync(student);
            await context.SaveChangesAsync();

            return Ok(new { isInsert = student.StudentName});
        }

        [HttpGet]
        public async Task<IActionResult> GetAbsent()
        {
            return Ok(await context.Absent.ToListAsync());
        }
    }
}