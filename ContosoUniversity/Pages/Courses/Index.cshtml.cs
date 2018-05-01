using System.Collections.Generic;
using System.Threading.Tasks;
using ContosoUniversity.Models.SchoolViewModels;
using ContosoUniversity.Pages.Instructors;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Pages.Courses
{
    public class IndexModel : PageModel
    {
        private readonly Data.SchoolContext _context;

        public IndexModel(Data.SchoolContext context)
        {
            _context = context;
        }

        public IList<CourseViewModel> CourseVm { get;set; }

        public async Task OnGetAsync()
        {
            CourseVm = await new CourseMapper().ManyTo(_context.Courses).ToListAsync();
        }
    }
}
