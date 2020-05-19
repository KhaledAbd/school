using school.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace school.dtos
{
    public class AbsentCelander
    {
        public DateTime startDate { get; set; }

        public int NumOfDay { get; set; }

        public int month { get; set; }

        public bool?[,] absentMat; 
        public void SetNumOfDay()
        {
            DateTime dt = DateTime.Today;
            if (month < 7) {
                startDate = new DateTime(dt.Year, month, 1);
            }
            else
            {
                startDate = new DateTime(dt.Year - 1, month, 1);
            }
            // var month = startDate.Month;
            var enddate = startDate.AddMonths(1).AddDays(-1);
            NumOfDay = 1 + enddate.Day - startDate.Day;
        }

        public void  MatrixSet(IEnumerable<Student> students, SchoolContext context)
        {
            int count = students.Count();
            absentMat = new bool?[count, NumOfDay];
            Students = new string[count];
            foreach (Student student in students)
            {
                int i = student.StudentId % count;
                Students[i] = student.StudentName;

                foreach (var absent in context.Absent.Where(a=>a.StudentId == student.StudentId && a.DateAbsent.Month == month)) { 
                            int j = absent.DateAbsent.Day - 1;
                            absentMat[ i, j] = absent.AbsentCheck;
                }
                
            }
        }
        public string[] Students { get; set;}
    }
}
