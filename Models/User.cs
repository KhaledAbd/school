using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace school.Models
{
    public class User
    {
        
        public string Name { get; set; }
        [Key]
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
