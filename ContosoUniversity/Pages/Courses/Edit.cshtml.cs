using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Models.SchoolViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Pages.Courses
{
    public class EditModel : DepartmentNamePageModel
    {
        private readonly ContosoUniversity.Data.SchoolContext _context;

        public EditModel(ContosoUniversity.Data.SchoolContext context)
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

            Course = await _context.Courses.Select(c => new CourseViewModel
                {
                    CourseId = c.CourseId,
                    Credits = c.Credits,
                    Title = c.Title,
                    Department = new DepartmentViewModel
                    {
                        Id = c.Department.DepartmentId,
                        Name = c.Department.Name                        
                    },
                    Enrollments = c.Enrollments.Select(e => new EnrollmentViewModel
                    {
                        Grade = e.Grade,
                        StudentName = e.Student.FullName,
                        CourseTitle = c.Title
                    })
                })
                .FirstOrDefaultAsync(m => m.CourseId == id);

            if (Course == null)
            {
                return NotFound();
            }

            // Select current DepartmentId
            PopulateDepartmentsDropDownList(_context, Course.Department.Id);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var courseToUpdate = await _context.Courses.FindAsync(id);

            if (await TryUpdateModelAsync(
                courseToUpdate,
                "course",
                c => c.Credits,
                c => c.DepartmentId,
                c => c.Title))
            {
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }

            // Select DepartmentId if TryUpdateModelAsync fails
            PopulateDepartmentsDropDownList(_context, courseToUpdate.DepartmentId);

            return Page();
        }
    }
}
