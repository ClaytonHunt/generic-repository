using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ContosoUniversity.Models.SchoolViewModels;
using System.Linq;

namespace ContosoUniversity.Pages.Departments
{
    public class DeleteModel : PageModel
    {
        private readonly ContosoUniversity.Data.SchoolContext _context;

        public DeleteModel(ContosoUniversity.Data.SchoolContext context)
        {
            _context = context;
        }

        [BindProperty]
        public DepartmentViewModel Department { get; set; }
        public string ConcurrencyErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int id, bool? concurrencyError)
        {
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

            if (concurrencyError.GetValueOrDefault())
            {
                ConcurrencyErrorMessage = "The record you attempted to delete "
                  + "was modified by another user after you selected delete. "
                  + "The delete operation was canceled and the current values in the "
                  + "database have been displayed. If you still want to delete this "
                  + "record, click the Delete button again.";
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            try
            {
                var departmentToDelete = await _context.Departments.FirstOrDefaultAsync(m => m.DepartmentId == id);

                if (departmentToDelete != null)
                {
                    // Department.rowVersion value is from when the entity
                    // was fetched. If it doesn't match the DB, a
                    // DbUpdateConcurrencyException exception is thrown.
                    _context.Departments.Remove(departmentToDelete);
                    await _context.SaveChangesAsync();
                }

                return RedirectToPage("./Index");
            }
            catch (DbUpdateConcurrencyException)
            {
                return RedirectToPage("./Delete",
                    new { concurrencyError = true, id = id });
            }
        }
    }
}