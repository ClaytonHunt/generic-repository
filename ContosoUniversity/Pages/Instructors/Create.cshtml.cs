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
        private readonly Data.SchoolContext _context;

        public CreateModel(Data.SchoolContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGet()
        {
            var instructor = new InstructorViewModel
            {
                CourseAssignments = new List<CourseAssignmentViewModel>()
            };

            var allCourses = await new CourseService(_context).GetAllAsync();

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

           if (selectedCourses != null)
            {
                Instructor.CourseAssignments = new List<CourseAssignmentViewModel>();

                foreach (var course in selectedCourses)
                {
                    var courseToAdd = new CourseAssignmentViewModel
                    {
                        Course = new CourseViewModel
                        {
                            CourseId = int.Parse(course)
                        }
                    };

                    ((IList<CourseAssignmentViewModel>)Instructor.CourseAssignments).Add(courseToAdd);
                }
            }

            await new InstructorService(_context).CreateAsync(Instructor);

            return RedirectToPage("./Index");
        }
    }
}