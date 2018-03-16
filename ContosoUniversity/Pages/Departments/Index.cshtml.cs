using System.Collections.Generic;
using System.Threading.Tasks;
using ContosoUniversity.Models.SchoolViewModels;
using ContosoUniversity.Pages.Instructors;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Pages.Departments
{
    public class IndexModel : PageModel
    {
        private readonly ContosoUniversity.Data.SchoolContext _context;

        public IndexModel(ContosoUniversity.Data.SchoolContext context)
        {
            _context = context;
        }

        public IList<DepartmentViewModel> Department { get;set; }

        public async Task OnGetAsync()
        {
            Department = await new DepartmentMapper().ManyTo(_context.Departments).ToListAsync();
        }
    }
}
