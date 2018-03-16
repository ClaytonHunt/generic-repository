using System.Linq;
using ContosoUniversity.Models;
using ContosoUniversity.Models.SchoolViewModels;

namespace ContosoUniversity.Pages.Instructors
{
    public class CourseMapper
    {
        public CourseViewModel SingleTo(Course course)
        {
            return new CourseViewModel
            {
                CourseId = course.CourseId,
                Credits = course.Credits,
                Title = course.Title,
                Department = course.Department == null ? null : new DepartmentViewModel
                {
                    Id = course.Department.DepartmentId,
                    Name = course.Department.Name
                },
                Enrollments = course.Enrollments?.Select(e => new EnrollmentViewModel
                {
                    Grade = e.Grade,
                    StudentName = e.Student.FullName,
                    CourseTitle = course.Title
                })
            };
        }

        public IQueryable<CourseViewModel> ManyTo(IQueryable<Course> courses)
        {
            return courses.Select(c => new CourseViewModel
            {
                CourseId = c.CourseId,
                Credits = c.Credits,
                Title = c.Title,
                Department = new DepartmentViewModel
                {
                    Id = c.Department.DepartmentId,
                    Name = c.Department.Name
                },
                Enrollments = c.Enrollments.Select(e => new EnrollmentViewModel
                {
                    Grade = e.Grade,
                    StudentName = e.Student.FullName,
                    CourseTitle = c.Title
                })
            });
        }

        public Course SingleFrom(CourseViewModel course)
        {
            return new Course
            {
                CourseId = course.CourseId,
                Credits = course.Credits,
                DepartmentId = course.Department.Id,
                Title = course.Title
            };
        }
    }
}
