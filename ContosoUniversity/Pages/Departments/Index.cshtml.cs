using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Models.SchoolViewModels;
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
            Department = await _context.Departments.Select(d => new DepartmentViewModel
            {
                Id = d.DepartmentId,
                Name = d.Name,
                Budget = d.Budget,
                StartDate = d.StartDate,
                Version = d.RowVersion[7],
                Administrator = d.Administrator == null ? null : new InstructorViewModel
                {
                    Id = d.Administrator.Id,
                    FirstMidName = d.Administrator.FirstMidName,
                    LastName = d.Administrator.LastName                    
                }
            }).ToListAsync();
        }
    }
}
