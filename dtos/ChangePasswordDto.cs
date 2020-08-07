using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace school.dtos
{
    public class ChangePasswordDto
    {
        public string NewPassword { get; set; }
        public string OldPassword { get; set; }
    }
}
