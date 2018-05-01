using System.Threading.Tasks;
using ContosoUniversity.Models.SchoolViewModels;
using ContosoUniversity.Pages.Instructors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Pages.Courses
{
    public class DeleteModel : PageModel
    {
        private readonly Data.SchoolContext _context;

        public DeleteModel(Data.SchoolContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CourseViewModel Course { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Course = await new CourseMapper().ManyTo(_context.Courses).FirstOrDefaultAsync(m => m.CourseId == id);

            if (Course == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseToDelete = await new CourseMapper().ManyTo(_context.Courses.AsNoTracking()).FirstOrDefaultAsync(m => m.CourseId == id);

            if (courseToDelete != null)
            {
                _context.Courses.Remove(new CourseMapper().SingleFrom(courseToDelete));
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
