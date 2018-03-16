using System.Threading.Tasks;
using ContosoUniversity.Data;
using ContosoUniversity.Models.SchoolViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Pages.Students
{
    public class DeleteModel : PageModel
    {
        private readonly SchoolContext _context;

        public DeleteModel(SchoolContext context)
        {
            _context = context;
        }

        [BindProperty]
        public StudentViewModel Student { get; set; }
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, bool saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            Student = new StudentMapper().SingleTo(await _context.Students.FirstOrDefaultAsync(m => m.Id == id));

            if (Student == null)
            {
                return NotFound();
            }

            if (saveChangesError)
            {
                ErrorMessage = "Delete failed. Try again";
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var student = new StudentMapper().SingleTo(await _context.Students.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id));

            if (student == null)
            {
                return NotFound();
            }

            try
            {
                _context.Students.Remove(new StudentMapper().SingleFrom(student));
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }
            catch (DbUpdateException)
            {
                // Log the error (uncomment ex variable name and write a log.
                return RedirectToAction("./Delete", new { id, saveChangesError = true });
            }
        }
    }
}
