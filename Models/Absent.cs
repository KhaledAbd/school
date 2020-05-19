using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace school.Models
{
    public class Absent
    {
        public Absent()
        {
            DateAbsent = DateTime.Now;
        }
        [Key]
        [ForeignKey("StudentNavigation")]
        public int StudentId { get; set; }
        public DateTime DateAbsent { get; set; }
        [Display(Name = "الغيــاب")]

        public bool? AbsentCheck { get; set; }

        public virtual Student StudentNavigation { get; set; }
    }
}
