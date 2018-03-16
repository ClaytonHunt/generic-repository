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
                Instructors = await new InstructorMapper().ManyTo(_context.Instructors)
                    .OrderBy(i => i.LastName)
                    .ToListAsync()
            };

            if (id != null)
            {
                InstructorId = id.Value;

                var instructor = Instructor.Instructors.Single(i => i.Id == id.Value);

                Instructor.Courses = instructor.CourseAssignments.Select(s => s.Course);
            }

            if (courseId != null)
            {
                CourseId = courseId.Value;
                var selectedCourse = Instructor.Courses.Single(x => x.CourseId == courseId);

                selectedCourse.Enrollments = await new EnrollmentMapper().ManyTo(_context.Enrollments.Where(e => e.CourseId == selectedCourse.CourseId)).ToListAsync();

                Instructor.Enrollments = selectedCourse.Enrollments;
            }
        }
    }
}