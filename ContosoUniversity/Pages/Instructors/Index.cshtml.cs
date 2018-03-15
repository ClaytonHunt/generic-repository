using ContosoUniversity.Models;
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
            Instructor = new InstructorIndexData();
            Instructor.Instructors = await _context.Instructors
                .Include(i => i.OfficeAssignment)
                .Include(i => i.CourseAssignments)
                    .ThenInclude(i => i.Course)
                        .ThenInclude(i => i.Department)
//                    .Include(i => i.CourseAssignments)
//                        .ThenInclude(i => i.Course)
//                            .ThenInclude(i => i.Enrollments)
//                                .ThenInclude(i => i.Student)
//                .AsNoTracking()
                .OrderBy(i => i.LastName)
                .ToListAsync();

            if (id != null)
            {
                InstructorId = id.Value;
                Instructor instructor = Instructor.Instructors.Single(i => i.Id == id.Value);
                Instructor.Courses = instructor.CourseAssignments.Select(s => s.Course);
            }

            if (courseId != null)
            {
                CourseId = courseId.Value;
                var selectedCourse = Instructor.Courses.Single(x => x.CourseId == courseId);
                await _context.Entry(selectedCourse).Collection(x => x.Enrollments).LoadAsync();

                foreach (var enrollment in selectedCourse.Enrollments)
                {
                    await _context.Entry(enrollment).Reference(x => x.Student).LoadAsync();
                }

                Instructor.Enrollments = selectedCourse.Enrollments;
            }
        }
    }
}