using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
