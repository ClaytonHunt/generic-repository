using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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

            var allCourses = new CourseMapper().ManyTo(_context.Courses);

            // Provides an empty collection for the foreach loop
            // foreach (var course in Model.AssignedCourseDataList)
            // in the Create Razor page.
            PopulateAssignedCourseData(allCourses, instructor);

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

            _context.Instructors.Add(new InstructorMapper().SingleFrom(Instructor));

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}