using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace school.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        [Display(Name = "اسم الطالب")]

        public string StudentName { get; set; }

        [Display(Name = "الغياب")]

        public int? TimeAbsent { get; set; }
        [Display(Name = "الفصل")]

        [ForeignKey("ClassNavigation")]
        public int? ClassFk { get; set; }

        public int TimeAbsentDaily { get; set; }

        public bool IsReject { get; set; }

        public int NumOfReject { get; set; }
        public virtual Class ClassNavigation { get; set; }
        public virtual ICollection<Absent> AbsentNavigation { get; set; }
    }
}
