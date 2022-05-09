using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class Course
    {
        public int CourseId { get; private set; }
        public int Number { get; set; }
        public int Division { get; set; }
        public string Level { get; set; }
        public string Shift { get; set; }
        public int Year { get; set; }
        public ICollection<Student>? Students { get; set; }
    }
}
