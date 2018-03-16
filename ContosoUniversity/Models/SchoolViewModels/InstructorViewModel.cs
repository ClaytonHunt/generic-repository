using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.Models.SchoolViewModels
{
    public class InstructorViewModel
    {
        public int Id { get; set; }
        public DateTime HireDate { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public OfficeAssignmentViewModel OfficeAssignment { get; set; }
        public IEnumerable<CourseAssignmentViewModel> CourseAssignments { get; set; }

        [Display(Name = "Full Name")]
        public string FullName => $"{FirstMidName} {LastName}";
    }
}
