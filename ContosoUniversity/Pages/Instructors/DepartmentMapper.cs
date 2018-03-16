using System.Collections.Generic;
using System.Linq;
using ContosoUniversity.Models;
using ContosoUniversity.Models.SchoolViewModels;

namespace ContosoUniversity.Pages.Instructors
{
    public class DepartmentMapper
    {
        public DepartmentViewModel SingleTo(Department department)
        {
            return department == null ? null : new DepartmentViewModel
            {
                Id = department.DepartmentId,
                Budget = department.Budget,
                Name = department.Name,
                StartDate = department.StartDate,    
                Administrator = department.InstructorId == null ? null : new InstructorViewModel
                {
                    Id = department.InstructorId ?? 0
                } 
            };
        }

        public IQueryable<DepartmentViewModel> ManyTo(IQueryable<Department> departments)
        {
            return departments?.Select(d => new DepartmentViewModel
            {
                Id = d.DepartmentId,
                Name = d.Name,
                Budget = d.Budget,
                StartDate = d.StartDate,
                Administrator = d.Administrator == null ? null : new InstructorViewModel
                {
                    Id = d.Administrator.Id,
                    FirstMidName = d.Administrator.FirstMidName,
                    LastName = d.Administrator.LastName,
                    HireDate = d.Administrator.HireDate,
                    OfficeAssignment = d.Administrator.OfficeAssignment == null ? null : new OfficeAssignmentViewModel
                    {
                        Location = d.Administrator.OfficeAssignment.Location
                    },
                    CourseAssignments = d.Administrator.CourseAssignments == null ? null : d.Administrator.CourseAssignments.Select(ca => new CourseAssignmentViewModel
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
                            Enrollments = ca.Course.Department == null ? null : ca.Course.Enrollments.Select(e => new EnrollmentViewModel
                            {
                                Grade = e.Grade,
                                CourseTitle = ca.Course.Title,
                                StudentName = e.Student.FullName
                            })
                        }
                    })
                }
            });
        }

        public Department SingleFrom(DepartmentViewModel department)
        {
            return new Department
            {
                DepartmentId = department.Id,
                InstructorId = department.Administrator?.Id,
                Budget = department.Budget,
                Name = department.Name,
                StartDate = department.StartDate,                 
            };
        }

        public IEnumerable<Department> ManyFrom(IEnumerable<DepartmentViewModel> departments)
        {
            return departments.Select(SingleFrom);
        }
    }
}
