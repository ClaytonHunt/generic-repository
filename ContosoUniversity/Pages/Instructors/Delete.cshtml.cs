using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Models.SchoolViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Pages.Instructors
{
    public class DeleteModel : PageModel
    {
        private readonly ContosoUniversity.Data.SchoolContext _context;

        public DeleteModel(ContosoUniversity.Data.SchoolContext context)
        {
            _context = context;
        }

        [BindProperty]
        public InstructorViewModel Instructor { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Instructor = await _context.Instructors.Select(i => new InstructorViewModel
            {
                Id = i.Id,
                FirstMidName = i.FirstMidName,
                LastName = i.LastName,
                HireDate = i.HireDate,
                OfficeAssignment = new OfficeAssignmentViewModel
                {
                    Location = i.OfficeAssignment.Location
                },
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
                        },
                        Enrollments = ca.Course.Enrollments.Select(e => new EnrollmentViewModel
                        {
                            Grade = e.Grade,
                            CourseTitle = e.Course.Title,
                            StudentName = e.Student.FullName
                        })
                    }
                })
            }).FirstOrDefaultAsync(m => m.Id == id);

            if (Instructor == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var instructor = await _context.Instructors
                .Include(i => i.CourseAssignments)
                .SingleAsync(i => i.Id == id);

            var departments = await _context.Departments
                .Where(d => d.InstructorId == id)
                .ToListAsync();

            departments.ForEach(d => d.InstructorId = null);

            _context.Instructors.Remove(instructor);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
