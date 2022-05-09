using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class Student
    {
        public int StudentId { get; private set; }
        public int DocumentNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int? CourseId { get; set; }
        public Course? Course { get; set; }

        public Student() { }
        public Student(int documentNumber, string firstName, string lastName, DateTime dateOfBirth)
        {
            DocumentNumber = documentNumber;
            FirstName = firstName.TrimEnd();
            LastName = lastName.TrimEnd();
            FullName = $"{LastName} {FirstName}";
            DateOfBirth = dateOfBirth;
        }
    }
}
