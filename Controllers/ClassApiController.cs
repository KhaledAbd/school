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
    public class ClassApiController : ControllerBase
    {
        private readonly SchoolContext context;
        public ClassApiController(SchoolContext context)
        {
            this.context = context;
        }
        [HttpPost]
        public async Task<IActionResult> PostClass(Class cls)
        {
            bool isInsert = false;
            if(cls != null)
            {
                await context.Class.AddAsync(cls);
                await context.SaveChangesAsync();
                isInsert = true;
            }
            return Ok(new { isInsert = isInsert });
        }
    }
}