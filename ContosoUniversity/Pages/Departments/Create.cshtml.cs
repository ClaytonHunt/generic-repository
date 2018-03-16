using System.Threading.Tasks;
using ContosoUniversity.Models.SchoolViewModels;
using ContosoUniversity.Pages.Instructors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ContosoUniversity.Pages.Departments
{
    public class CreateModel : PageModel
    {
        private readonly ContosoUniversity.Data.SchoolContext _context;

        public SelectList Instructors { get; set; }

        public CreateModel(ContosoUniversity.Data.SchoolContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            Instructors = new SelectList(new InstructorMapper().ManyTo(_context.Instructors), "Id", "FullName");

            return Page();
        }

        [BindProperty]
        public DepartmentViewModel Department { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Departments.Add(new DepartmentMapper().SingleFrom(Department));
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}