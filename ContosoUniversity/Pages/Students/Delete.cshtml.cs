using System;
using System.Threading.Tasks;
using ContosoUniversity.Data;
using ContosoUniversity.Models.SchoolViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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

            Student = await new StudentService(_context).GetByIdAsync(id.Value);

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
            var student = await new StudentService(_context).GetByIdAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            try
            {
                await new StudentService(_context).DeleteAsync(student);

                return RedirectToPage("./Index");
            }
            catch (Exception)
            {
                // Log the error (uncomment ex variable name and write a log.
                return RedirectToAction("./Delete", new { id, saveChangesError = true });
            }
        }
    }
}
