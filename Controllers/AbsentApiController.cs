using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using school.Models;

namespace school.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AbsentApiController : ControllerBase
    {
        private readonly SchoolContext context;
        public AbsentApiController(SchoolContext context)
        {
            this.context = context;
        }

        // /api/AbsentApi/setAbsent
        [HttpPost("setAbsent")]
        public async Task<IActionResult> GetAbsent(IEnumerable<Absent> absents)
        {
            await context.AddRangeAsync(absents);
            SaveStudentAbsent(absents);
            await context.SaveChangesAsync();


            return Ok(new { isInsert = "تم اضافة بنجاح" });
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
                        student.TimeAbsent = 0;
                    }
                }
            }
        }
        // /api/AbsentApi/setStudent
        [HttpPost("setStudent")]
        public async Task<IActionResult> AddSudent(Student student)
        {
            if (student == null)
                return Unauthorized();
            int? Cls;
            if (HttpContext.Session.GetInt32("Stage") != null){
                if((Cls = HttpContext.Session.GetInt32("Class")) != null){
                    student.ClassFk = Cls;
                    student.TimeAbsent = 0;
                }
            }
            await context.AddAsync(student);
            await context.SaveChangesAsync();

            return Ok(new { isInsert = "تم الادراج بنجاح" });
        }

    }
}