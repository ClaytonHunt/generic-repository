using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Models.SchoolViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Pages.Instructors
{
    public class EditModel : InstructorCoursesPageModel
    {
        private readonly ContosoUniversity.Data.SchoolContext _context;

        public EditModel(ContosoUniversity.Data.SchoolContext context)
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

            Instructor = await _context.Instructors
                    .Select(i => new InstructorViewModel
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
                                Enrollments = ca.Course.Enrollments.Select(e => new EnrollmentViewModel
                                {
                                    Grade = e.Grade,
                                    CourseTitle = e.Course.Title,
                                    StudentName = e.Student.FullName
                                })
                            }
                        })
                    })
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Instructor == null)
            {
                return NotFound();
            }

            PopulateAssignedCourseData(_context, Instructor);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedCourses)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var instructorToUpdate = await _context.Instructors.Select(i => new InstructorViewModel
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
                        Enrollments = ca.Course.Enrollments.Select(e => new EnrollmentViewModel
                        {
                            Grade = e.Grade,
                            CourseTitle = e.Course.Title,
                            StudentName = e.Student.FullName
                        })
                    }

                })
            })
                .FirstOrDefaultAsync(i => i.Id == id);

            if (await TryUpdateModelAsync(
                instructorToUpdate,
                "Instructor",
                i => i.FirstMidName,
                i => i.LastName,
                i => i.HireDate,
                i => i.OfficeAssignment))
            {
                if (string.IsNullOrWhiteSpace(instructorToUpdate.OfficeAssignment?.Location))
                {
                    instructorToUpdate.OfficeAssignment = null;
                }

                UpdateInstructorCourses(_context, selectedCourses, instructorToUpdate);

                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }

            UpdateInstructorCourses(_context, selectedCourses, instructorToUpdate);
            PopulateAssignedCourseData(_context, instructorToUpdate);

            return RedirectToPage("./Index");
        }
    }
}
