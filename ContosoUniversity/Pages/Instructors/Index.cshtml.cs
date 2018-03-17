using ContosoUniversity.Models.SchoolViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Pages.Instructors
{
    public class IndexModel : PageModel
    {
        private readonly ContosoUniversity.Data.SchoolContext _context;

        public IndexModel(ContosoUniversity.Data.SchoolContext context)
        {
            _context = context;
        }

        public InstructorIndexData Instructor { get; set; }
        public int InstructorId { get; set; }
        public int CourseId { get; set; }

        public async Task OnGetAsync(int? id, int? courseId)
        {
            Instructor = new InstructorIndexData
            {
                Instructors = await new InstructorService(_context).GetAllAsync()
                    .OrderBy(i => i.LastName)
                    .ToListAsync()
            };

            if (id != null)
            {
                SelectInstructor(id);
            }

            if (courseId != null)
            {
                await SelectCourse(courseId.Value);
            }
        }

        private async Task SelectCourse(int courseId)
        {
            CourseId = courseId;

            var selectedCourse = Instructor.Courses.Single(x => x.CourseId == courseId);

            selectedCourse.Enrollments = await new EnrollmentService(_context).GetAllAsync(e => e.Where(x => x.CourseId == selectedCourse.CourseId));

            Instructor.Enrollments = selectedCourse.Enrollments;
        }

        private void SelectInstructor(int? id)
        {
            InstructorId = id.Value;

            var instructor = Instructor.Instructors.Single(i => i.Id == id.Value);

            Instructor.Courses = instructor.CourseAssignments.Select(s => s.Course);
        }
    }
}