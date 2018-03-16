using System.Collections.Generic;
using System.Threading.Tasks;
using ContosoUniversity.Data;
using ContosoUniversity.Models.SchoolViewModels;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Pages.Instructors
{
    public class CourseService
    {
        private readonly SchoolContext _context;

        public CourseService(SchoolContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CourseViewModel>> GetAllAsync()
        {
            return await new CourseMapper().ManyTo(_context.Courses).ToListAsync();
        }
    }
}