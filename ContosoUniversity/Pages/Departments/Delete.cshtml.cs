using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ContosoUniversity.Models.SchoolViewModels;
using System.Linq;
using ContosoUniversity.Pages.Instructors;

namespace ContosoUniversity.Pages.Departments
{
    public class DeleteModel : PageModel
    {
        private readonly ContosoUniversity.Data.SchoolContext _context;

        public DeleteModel(ContosoUniversity.Data.SchoolContext context)
        {
            _context = context;
        }

        [BindProperty]
        public DepartmentViewModel Department { get; set; }
        public string ConcurrencyErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int id, bool? concurrencyError)
        {
            Department = await new DepartmentMapper().ManyTo(_context.Departments)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Department == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            try
            {
                var departmentToDelete = new DepartmentMapper().SingleTo(await _context.Departments.AsNoTracking().FirstOrDefaultAsync(m => m.DepartmentId == id));

                if (departmentToDelete != null)
                {
                    _context.Departments.Remove(new DepartmentMapper().SingleFrom(departmentToDelete));

                    await _context.SaveChangesAsync();
                }

                return RedirectToPage("./Index");
            }
            catch (DbUpdateConcurrencyException)
            {
                return RedirectToPage("./Delete",
                    new { concurrencyError = true, id = id });
            }
        }
    }
}