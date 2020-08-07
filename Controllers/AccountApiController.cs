using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using school.dtos;
using school.Models;
using System.Linq;
using System.Threading.Tasks;

namespace school.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountApiController : ControllerBase
    {
        private readonly SchoolContext context;
        public AccountApiController(SchoolContext context)
        {
            this.context = context;
        }
        [HttpPut]
        [Route("changepassword")]

        //// api/account/changepassword
        public async Task<IActionResult> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            if (string.IsNullOrEmpty(changePasswordDto.OldPassword) || string.IsNullOrEmpty(changePasswordDto.NewPassword))
            {
                ModelState.AddModelError("", "لم يتم تغير الباسورد");
                return Unauthorized();
            }
            var user = await context.User.FirstOrDefaultAsync();
            if (string.Equals(user.Password, changePasswordDto.OldPassword))
            {
                    user.Password = changePasswordDto.NewPassword;
                await context.SaveChangesAsync();
            }
            return Ok(new { isUpdate = 1 });
        }
        [HttpPut]
        [Route("change/{oldusername}")]
        //// api/account/changeusername

        public async Task<IActionResult> ChangeUserName(string oldusername, [FromBody] string username)
        {
            if (username == null)
                return Unauthorized();
            if (username.Length < 5)
                return Unauthorized();
            var user = await context.User.Where(u => u.UserName == oldusername).FirstOrDefaultAsync();
            if(user == null)
            {
                return NotFound();
            }
            user.UserName = username;
            await context.SaveChangesAsync();
            return Ok(new { isUpdate = username });
        }
    }
}