using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Models.SchoolViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Pages.Departments
{
    public class DetailsModel : PageModel
    {
        private readonly ContosoUniversity.Data.SchoolContext _context;

        public DetailsModel(ContosoUniversity.Data.SchoolContext context)
        {
            _context = context;
        }

        public DepartmentViewModel Department { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Department = await _context.Departments.Select(d => new DepartmentViewModel
            {
                Id = d.DepartmentId,
                Version = d.RowVersion[7],
                Name = d.Name,
                Budget = d.Budget,
                StartDate = d.StartDate,
                Administrator = new InstructorViewModel
                {
                    Id = d.Administrator.Id,
                    FirstMidName = d.Administrator.FirstMidName,
                    LastName = d.Administrator.LastName,
                    HireDate = d.Administrator.HireDate,
                    OfficeAssignment = new OfficeAssignmentViewModel
                    {
                        Location = d.Administrator.OfficeAssignment.Location
                    },
                    CourseAssignments = d.Administrator.CourseAssignments.Select(ca => new CourseAssignmentViewModel
                    {
                        Course = new CourseViewModel
                        {
                            CourseId = ca.Course.CourseId,
                            Title = ca.Course.Title,
                            Credits = ca.Course.Credits,
                            Department = new DepartmentViewModel
                            {
                                Name = ca.Course.Department.Name
                            },
                            Enrollments = ca.Course.Enrollments.Select(e => new EnrollmentViewModel
                            {
                                Grade = e.Grade,
                                CourseTitle = ca.Course.Title,
                                StudentName = e.Student.FullName
                            })
                        }
                    })
                }
            })
            .FirstOrDefaultAsync(m => m.Id == id);

            if (Department == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
