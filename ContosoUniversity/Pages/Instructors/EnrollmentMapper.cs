using System;
using System.Collections.Generic;
using System.Linq;
using ContosoUniversity.Models;
using ContosoUniversity.Models.SchoolViewModels;

namespace ContosoUniversity.Pages.Instructors
{
    public class EnrollmentMapper
    {
        public EnrollmentViewModel SingleTo(Enrollment enrollment)
        {
            return null;
        }

        public IQueryable<EnrollmentViewModel> ManyTo(IQueryable<Enrollment> enrollments)
        {
            return enrollments.Select(e => new EnrollmentViewModel
            {
                StudentName = e.Student.FullName,
                Grade = e.Grade,
                CourseId = e.CourseId,
                CourseTitle = e.Course.Title
            });
        }

        public Enrollment SingleFrom(EnrollmentViewModel enrollment)
        {
            return null;
        }
    }
}
