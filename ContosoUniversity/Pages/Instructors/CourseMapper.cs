using System;
using System.Collections.Generic;
using System.Linq;
using ContosoUniversity.Models;
using ContosoUniversity.Models.SchoolViewModels;

namespace ContosoUniversity.Pages.Instructors
{
    public class CourseMapper
    {
        public CourseViewModel SingleTo(Course course)
        {
            return null;
        }

        public IQueryable<CourseViewModel> ManyTo(IQueryable<Course> courses)
        {
            return courses.Select(course => new CourseViewModel
            {
                CourseId = course.CourseId
            });
        }

        public Course SingleFrom(CourseViewModel course)
        {
            return null;
        }
    }
}
