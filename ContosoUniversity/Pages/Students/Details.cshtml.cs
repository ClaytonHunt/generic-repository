using System.Threading.Tasks;
using ContosoUniversity.Models.SchoolViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContosoUniversity.Pages.Students
{
    public class DetailsModel : PageModel
    {
        private readonly StudentService _service;

        public DetailsModel(StudentService service)
        {
            _service = service;
        }

        public StudentViewModel Student { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
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

            return Page();
        }
    }
}
