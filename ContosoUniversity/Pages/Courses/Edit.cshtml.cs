using System;
using System.Threading.Tasks;
using ContosoUniversity.Models.SchoolViewModels;
using ContosoUniversity.Pages.Instructors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Pages.Courses
{
    public class EditModel : DepartmentNamePageModel
    {
        public EditModel(Data.SchoolContext context) : base(context)
        {
            
        }

        [BindProperty]
        public CourseViewModel Course { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Course = await new CourseMapper().ManyTo(Context.Courses).FirstOrDefaultAsync(m => m.CourseId == id);

            if (Course == null)
            {
                return NotFound();
            }

            PopulateDepartmentsDropDownList(selectedDepartment: Course.Department.Id);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                Context.Courses.Update(new CourseMapper().SingleFrom(Course));
                await Context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }
            catch (Exception)
            {
                PopulateDepartmentsDropDownList(selectedDepartment: Course.Department.Id);

                return Page();
            }
        }
    }
}
