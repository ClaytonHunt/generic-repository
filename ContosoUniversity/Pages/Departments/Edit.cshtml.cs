using System;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Models;
using ContosoUniversity.Models.SchoolViewModels;
using ContosoUniversity.Pages.Instructors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Pages.Departments
{
    public class EditModel : PageModel
    {
        private readonly ContosoUniversity.Data.SchoolContext _context;

        public EditModel(ContosoUniversity.Data.SchoolContext context)
        {
            _context = context;
        }

        [BindProperty]
        public DepartmentViewModel Department { get; set; }
        public SelectList InstructorNames { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Department = await new DepartmentMapper().ManyTo(_context.Departments).FirstOrDefaultAsync(m => m.Id == id);

            if (Department == null)
            {
                return NotFound();
            }

            InstructorNames = new SelectList(new InstructorMapper().ManyTo(_context.Instructors), "Id", "FirstMidName");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var departmentToUpdate = await new DepartmentMapper().ManyTo(_context.Departments)
                .FirstOrDefaultAsync(d => d.Id == id);

            // null means Department was deleted by another user.
            if (departmentToUpdate == null)
            {
                return HandleDeletedDepartment();
            }

            departmentToUpdate.Name = Department.Name;
            departmentToUpdate.StartDate = Department.StartDate;
            departmentToUpdate.Budget = Department.Budget;
            departmentToUpdate.Administrator.Id = Department.Administrator.Id;

            _context.Departments.Update(new DepartmentMapper().SingleFrom(departmentToUpdate));

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private IActionResult HandleDeletedDepartment()
        {
            // ModelState contains the posted data because of the deletion
            // error and will override the Department instance values when displaying the Page().
            ModelState.AddModelError(string.Empty, "Unable to save. The department was deleted by another user.");
            InstructorNames = new SelectList(new InstructorMapper().ManyTo(_context.Instructors), "Id", "FullName", Department.Administrator.Id);

            return Page();
        }
    }
}
