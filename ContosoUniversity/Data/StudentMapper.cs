using System.Linq;
using ContosoUniversity.Models;
using ContosoUniversity.Models.SchoolViewModels;

namespace ContosoUniversity.Pages.Students
{
    public class StudentMapper : IMapper<StudentViewModel, Student>
    {
        public StudentViewModel SingleTo(Student item)
        {
            return new StudentViewModel
            {
                Id = item.Id,
                LastName = item.LastName,
                FirstMidName = item.FirstMidName,
                EnrollmentDate = item.EnrollmentDate
            };
        }

        public IQueryable<StudentViewModel> ManyTo(IQueryable<Student> items)
        {
            return items.Select(student => new StudentViewModel
            {
                Id = student.Id,
                LastName = student.LastName,
                FirstMidName = student.FirstMidName,
                EnrollmentDate = student.EnrollmentDate,
                Enrollments = student.Enrollments == null ? null : student.Enrollments.Select(e => new EnrollmentViewModel
                {
                    CourseTitle = e.Course.Title,
                    Grade = e.Grade
                })
            });
        }        

        public Student SingleFrom(StudentViewModel item)
        {
            return new Student
            {
                Id = item.Id,
                FirstMidName = item.FirstMidName,
                LastName = item.LastName,
                EnrollmentDate = item.EnrollmentDate
            };
        }        
    }
}
