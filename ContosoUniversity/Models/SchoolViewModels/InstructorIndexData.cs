using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Models.SchoolViewModels
{
    public class InstructorIndexData
    {
        public IEnumerable<InstructorViewModel> Instructors { get; set; }
        public IEnumerable<CourseViewModel> Courses { get; set; }
        public IEnumerable<EnrollmentViewModel> Enrollments { get; set; }
    }
}