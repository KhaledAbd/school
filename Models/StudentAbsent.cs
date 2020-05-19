using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace school.Models
{
    public class StudentAbsent
    {
        [Key]
        [ForeignKey("StudentNavigation")]
        public int Id { get; set; }
        public int Times { get; set; }
        public bool Is_rejected { set; get; }

        public StudentAbsent(int Id, int Times, bool Is_rejected)
        {
            this.Id = Id;
            this.Times = Times;
            this.Is_rejected = Is_rejected;
        }

        public virtual Student StudentNavigation { get; set; }
        
    }
}
