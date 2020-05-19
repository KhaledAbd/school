using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace school.Models
{
    public class Stage
    {
        [Key]
        public int StageId { get; set; }

        [Display(Name = "المرحلة الدراسية")]

        public string NameStage { get; set; }

        public virtual ICollection<Class> ClassNavigation { get; set; }
    }
}
