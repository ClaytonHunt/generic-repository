using System;
using System.Threading.Tasks;
using ContosoUniversity.Models.SchoolViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContosoUniversity.Pages.Students
{
    public class DeleteModel : PageModel
    {
        private readonly StudentService _service;

        public DeleteModel(StudentService service)
        {
            _service = service;
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

            Student = await _service.GetByIdAsync(id.Value);

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
            var student = await _service.GetByIdAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            try
            {
                await _service.DeleteAsync(student);

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
