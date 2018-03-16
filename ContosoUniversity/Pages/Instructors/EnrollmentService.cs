using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Data;
using ContosoUniversity.Models.SchoolViewModels;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Pages.Instructors
{
    public class EnrollmentService
    {
        private readonly SchoolContext _context;

        public EnrollmentService(SchoolContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EnrollmentViewModel>> GetAllAsync(Func<IQueryable<EnrollmentViewModel>, IQueryable<EnrollmentViewModel>> filterFunc)
        {
            filterFunc = filterFunc ?? (e => e);

            return await filterFunc(new EnrollmentMapper().ManyTo(_context.Enrollments)).ToListAsync();
        }
    }
}