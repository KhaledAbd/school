using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace school.Models
{
    public class Class
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "الفصل")]
        public string Name { get; set; }

        [Display(Name = "المرحلة الدراسية")]

        [ForeignKey("StageNavigation")]
        public int Stage { get; set; }

        public virtual Stage StageNavigation { get; set; }
        public virtual ICollection<Student> Student { get; set; }
    }
}
