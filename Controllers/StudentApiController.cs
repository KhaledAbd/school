using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using school.Models;

namespace school.Controllers
{
    [Route("api/student")]
    [ApiController]
    public class StudentApiController : ControllerBase
    {
        private readonly SchoolContext context;

        public StudentApiController(SchoolContext context)
        {
            this.context = context;
        }

        ///http://localhost:5000/api/student/delete/3/
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            var student = await context.Student.FirstOrDefaultAsync(s => s.StudentId == id);
            if(student == null)
            {
                return Unauthorized();
            }
            context.Remove(student);
            if(await context.SaveChangesAsync() != 0)
            {
                return Ok(new { isDelete = 1 });
            }
            throw new Exception();
        }
        ///   http://localhost:5000/api/student/transfer/3/
        [HttpPut]
        [Route("transfer/{id}")]
        public async Task<IActionResult> TransferStudent(Student student ,int? id)
        {
            if(student == null || student.StudentId != id.Value || id == null)
            {
                return Unauthorized();
            }
            var oldStd = await context.Student.SingleOrDefaultAsync(s => s.StudentId == student.StudentId);
            if(oldStd == null)
                return NotFound();
            oldStd.ClassFk = student.ClassFk;
            if (ModelState.IsValid)
            {
                await context.SaveChangesAsync();
                var clsName = context.Class.FirstOrDefault(c => c.Id == student.ClassFk).Name;
                return Ok(new { isUpdate = clsName });
            }
            throw new Exception();
        }
        /// http://localhost:5000/api/student/edit/3

        [HttpPut]
        [Route("edit/{id}")]
        public async Task<IActionResult> EditStudent(Student student, int? id)
        {
            if (student == null || student.StudentId != id || id == null)
            {
                return Unauthorized();
            }
            var oldStd = await context.Student.SingleOrDefaultAsync(s => s.StudentId == student.StudentId);
            if (oldStd == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                oldStd.StudentName = student.StudentName;
                await context.SaveChangesAsync();
                return Ok(new { isUpdate = oldStd.StudentName, studentId = student.StudentId });
            }
            throw new Exception();
        }
    }
}