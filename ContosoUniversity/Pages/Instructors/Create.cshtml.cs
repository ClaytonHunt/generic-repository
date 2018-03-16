using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using ContosoUniversity.Models;
using ContosoUniversity.Models.SchoolViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContosoUniversity.Pages.Instructors
{
    public class CreateModel : InstructorCoursesPageModel
    {
        private readonly ContosoUniversity.Data.SchoolContext _context;

        public CreateModel(ContosoUniversity.Data.SchoolContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            var instructor = new InstructorViewModel
            {
                CourseAssignments = new List<CourseAssignmentViewModel>()
            };

            // Provides an empty collection for the foreach loop
            // foreach (var course in Model.AssignedCourseDataList)
            // in the Create Razor page.
            PopulateAssignedCourseData(_context, instructor);

            return Page();
        }

        [BindProperty]
        public InstructorViewModel Instructor { get; set; }

        public async Task<IActionResult> OnPostAsync(string[] selectedCourses)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var newInstructor = new InstructorViewModel();

            if (selectedCourses != null)
            {
                foreach (var course in selectedCourses)
                {
                    var courseToAdd = new CourseAssignmentViewModel
                    {
                        Course = new CourseViewModel
                        {
                            CourseId = int.Parse(course)
                        }
                    };

                    ((IList<CourseAssignmentViewModel>)newInstructor.CourseAssignments).Add(courseToAdd);
                }
            }

            if (await TryUpdateModelAsync(
                newInstructor,
                "Instructor",
                i => i.FirstMidName,
                i => i.LastName,
                i => i.HireDate,
                i => i.OfficeAssignment))
            {
                _context.Instructors.Add(new Instructor()
                {
                    FirstMidName = newInstructor.FirstMidName,
                    LastName = newInstructor.LastName,
                    HireDate = newInstructor.HireDate,
                    OfficeAssignment = newInstructor.OfficeAssignment?.Location == null ? null : new OfficeAssignment
                    {
                        Location = newInstructor.OfficeAssignment.Location
                    }
                });
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }

            PopulateAssignedCourseData(_context, newInstructor);

            return Page();
        }
    }
}