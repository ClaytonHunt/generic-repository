using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Models.SchoolViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Pages.Instructors
{
    public class DeleteModel : PageModel
    {
        private readonly ContosoUniversity.Data.SchoolContext _context;

        public DeleteModel(ContosoUniversity.Data.SchoolContext context)
        {
            _context = context;
        }

        [BindProperty]
        public InstructorViewModel Instructor { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Instructor = new InstructorMapper().SingleTo(await _context.Instructors.FirstOrDefaultAsync(m => m.Id == id));

            if (Instructor == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var instructor = new InstructorMapper().SingleTo(await _context.Instructors.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id));

            if (instructor == null)
            {
                return NotFound();
            }

            var departments = await _context.Departments.Where(d => d.InstructorId == id).ToListAsync();
            departments.ForEach(d => d.InstructorId = null);
            _context.Departments.UpdateRange(departments);

            _context.Instructors.Remove(new InstructorMapper().SingleFrom(instructor));

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
