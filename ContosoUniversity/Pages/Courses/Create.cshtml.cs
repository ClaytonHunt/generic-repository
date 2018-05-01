using System.Threading.Tasks;
using ContosoUniversity.Models.SchoolViewModels;
using ContosoUniversity.Pages.Instructors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ContosoUniversity.Pages.Courses
{
    public class CreateModel : DepartmentNamePageModel
    {
        public CreateModel(Data.SchoolContext context) : base(context)
        {

        }

        public IActionResult OnGet()
        {
            PopulateDepartmentsDropDownList();

            return Page();
        }

        [BindProperty]
        public CourseViewModel Course { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Context.Courses.Add(new CourseMapper().SingleFrom(Course));

            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}