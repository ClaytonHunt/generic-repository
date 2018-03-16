using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Models;
using ContosoUniversity.Models.SchoolViewModels;
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
            Instructors = new SelectList(_context.Instructors.Select(i => new InstructorViewModel
            {
                Id = i.Id,
                FirstMidName = i.FirstMidName,
                LastName = i.LastName,
                HireDate = i.HireDate,
                OfficeAssignment = new OfficeAssignmentViewModel { },
                CourseAssignments = i.CourseAssignments.Select(ca => new CourseAssignmentViewModel
                {
                    Course = new CourseViewModel
                    {
                        CourseId = ca.Course.CourseId,
                        Title = ca.Course.Title,
                        Credits = ca.Course.Credits,
                        Department = new DepartmentViewModel
                        {
                            Name = ca.Course.Department.Name
                        } 
                    }
                })
            }), "Id", "FullName");

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

            var departmentToCreate = new Department
            {
                DepartmentId = Department.Id,
                Name = Department.Name,
                Budget = Department.Budget,
                InstructorId = Department.Administrator.Id,
                StartDate = Department.StartDate
            };

            _context.Departments.Add(departmentToCreate);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}