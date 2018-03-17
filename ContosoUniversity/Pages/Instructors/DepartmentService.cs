using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Data;
using ContosoUniversity.Models.SchoolViewModels;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Pages.Instructors
{
    public class DepartmentService
    {
        private readonly SchoolContext _context;

        public DepartmentService(SchoolContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DepartmentViewModel>> GetAllAsync(Func<IQueryable<DepartmentViewModel>, IQueryable<DepartmentViewModel>> filterFunc)
        {
            filterFunc = filterFunc ?? (d => d);

            return await filterFunc(new DepartmentMapper().ManyTo(_context.Departments.AsNoTracking())).ToListAsync();
        }

        public async Task UpdateMany(IEnumerable<DepartmentViewModel> departments)
        {
            _context.Departments.UpdateRange(new DepartmentMapper().ManyFrom(departments));

            await _context.SaveChangesAsync();
        }
    }
}