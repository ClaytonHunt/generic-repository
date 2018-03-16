using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Models;
using ContosoUniversity.Models.SchoolViewModels;

namespace ContosoUniversity.Pages.Students
{
    public class StudentMapper
    {
        public StudentViewModel SingleTo(Student student)
        {
            return new StudentViewModel
            {
                Id = student.Id,
                LastName = student.LastName,
                FirstMidName = student.FirstMidName,
                EnrollmentDate = student.EnrollmentDate
            };
        }

        public IQueryable<StudentViewModel> ManyTo(IQueryable<Student> students)
        {
            return students.Select(student => new StudentViewModel
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

        public Student SingleFrom(StudentViewModel student)
        {
            return new Student
            {
                Id = student.Id,
                FirstMidName = student.FirstMidName,
                LastName = student.LastName,
                EnrollmentDate = student.EnrollmentDate
            };
        }        
    }
}
