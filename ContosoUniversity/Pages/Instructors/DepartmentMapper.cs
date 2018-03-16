using System;
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
            return new DepartmentViewModel
            {
                Id = department.DepartmentId,
                Budget = department.Budget,
                Name = department.Name,
                StartDate = department.StartDate,             
                Version = department.RowVersion[7],
                Administrator = department.InstructorId == null ? null : new InstructorViewModel
                {
                    Id = department.InstructorId ?? 0
                } 
            };
        }

        public IQueryable<DepartmentViewModel> ManyTo(IQueryable<Department> departments)
        {
            return departments.Select(d => new DepartmentViewModel
            {
                Id = d.DepartmentId,
                Budget = d.Budget,
                Name = d.Name,
                StartDate = d.StartDate,             
                Version = d.RowVersion[7],
                Administrator = d.InstructorId == null ? null : new InstructorViewModel
                {
                    Id = d.InstructorId ?? 0
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
    }
}
