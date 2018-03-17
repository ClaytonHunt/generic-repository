using System;
using System.Collections.Generic;
using System.Linq;
using ContosoUniversity.Models;
using ContosoUniversity.Models.SchoolViewModels;

namespace ContosoUniversity.Pages.Instructors
{
    public class InstructorMapper
    {
        public InstructorViewModel SingleTo(Instructor instructor)
        {
            return new InstructorViewModel
            {
                Id = instructor.Id,
                FirstMidName = instructor.FirstMidName,
                LastName = instructor.LastName,
                HireDate = instructor.HireDate
            };
        }

        public IQueryable<InstructorViewModel> ManyTo(IQueryable<Instructor> instructors)
        {
            return instructors.Select(instructor => new InstructorViewModel
            {
                Id = instructor.Id,
                FirstMidName = instructor.FirstMidName,
                LastName = instructor.LastName,
                HireDate = instructor.HireDate,
                OfficeAssignment = instructor.OfficeAssignment == null ? null : new OfficeAssignmentViewModel
                {
                    Location = instructor.OfficeAssignment.Location
                },
                CourseAssignments = instructor.CourseAssignments == null ? null : instructor.CourseAssignments.Select(ca => new CourseAssignmentViewModel
                {
                    Course = ca.Course == null ? null : new CourseViewModel
                    {
                        CourseId = ca.Course.CourseId,
                        Title = ca.Course.Title,
                        Credits = ca.Course.Credits,
                        Department = ca.Course.Department == null ? null : new DepartmentViewModel
                        {
                            Name = ca.Course.Department.Name
                        },
                        Enrollments = ca.Course.Enrollments == null ? ca.Course.Enrollments.Select(e => new EnrollmentViewModel
                        {
                            Id = e.EnrollmentId,
                            Grade = e.Grade,
                            CourseTitle = ca.Course.Title,
                            StudentName = e.Student.FullName
                        }) : null
                    }
                })
            });
        }

        public Instructor SingleFrom(InstructorViewModel instructor)
        {
            return new Instructor
            {
                Id = instructor.Id,
                FirstMidName = instructor.FirstMidName,
                LastName = instructor.LastName,
                HireDate = instructor.HireDate,
                OfficeAssignment = instructor.OfficeAssignment?.Location == null ? null : new OfficeAssignment
                {
                    Location = instructor.OfficeAssignment.Location
                }
            };
        }
    }
}
