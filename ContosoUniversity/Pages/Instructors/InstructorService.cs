using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Data;
using ContosoUniversity.Models.SchoolViewModels;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Pages.Instructors
{
    public class InstructorService
    {
        private readonly SchoolContext _context;

        public InstructorService(SchoolContext context)
        {
            _context = context;
        }

        public IQueryable<InstructorViewModel> GetAllAsync()
        {
            return new InstructorMapper().ManyTo(_context.Instructors);
        }

        public async Task<InstructorViewModel> GetByIdAsync(int id)
        {
            return await new InstructorMapper().ManyTo(_context.Instructors).FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task UpdateAsync(InstructorViewModel instructor, string[] selectedCourses)
        {
            if (selectedCourses.Any())
            {
                var allCourses = new CourseMapper().ManyTo(_context.Courses);
                UpdateInstructorCourses(allCourses, selectedCourses, instructor);
            }

            _context.Instructors.Update(new InstructorMapper().SingleFrom(instructor));

            await _context.SaveChangesAsync();
        }

        public async Task Delete(InstructorViewModel instructor)
        {
            var departments = (await new DepartmentService(_context)
                    .GetAllAsync(d => d.Where(x => x.Administrator.Id == instructor.Id)))
                .ToList();

            foreach (var department in departments)
            {
                department.Administrator = null;
            }

            await new DepartmentService(_context).UpdateMany(departments);
            
            _context.Instructors.Remove(new InstructorMapper().SingleFrom(instructor));

            await _context.SaveChangesAsync();
        }

        public async Task CreateAsync(InstructorViewModel instructor)
        {
            _context.Instructors.Add(new InstructorMapper().SingleFrom(instructor));

            await _context.SaveChangesAsync();
        }

        private void UpdateInstructorCourses(IEnumerable<CourseViewModel> allCourses,
            string[] selectedCourses, InstructorViewModel instructorToUpdate)
        {
            if (selectedCourses == null)
            {
                instructorToUpdate.CourseAssignments = new List<CourseAssignmentViewModel>();
                return;
            }

            var selectedCoursesHS = new HashSet<string>(selectedCourses);
            var instructorCourses = new HashSet<int>(instructorToUpdate.CourseAssignments?.Select(c => c.Course.CourseId) ?? new List<int>());

            foreach (var course in allCourses)
            {
                if (selectedCoursesHS.Contains(course.CourseId.ToString()))
                {
                    if (!instructorCourses.Contains(course.CourseId))
                    {
                        instructorToUpdate.CourseAssignments = (instructorToUpdate.CourseAssignments ?? new List<CourseAssignmentViewModel>()).ToList();
                        ((IList<CourseAssignmentViewModel>)instructorToUpdate.CourseAssignments).Add(new CourseAssignmentViewModel
                        {
                            Instructor = instructorToUpdate,
                            Course = course
                        });
                    }
                }
                else
                {
                    if (instructorCourses.Contains(course.CourseId))
                    {
                        var courseToRemove = (instructorToUpdate.CourseAssignments ?? new List<CourseAssignmentViewModel>()).FirstOrDefault(c => c.Course.CourseId == course.CourseId);

                        if (courseToRemove != null)
                        {
                            ((IList<CourseAssignmentViewModel>)(instructorToUpdate.CourseAssignments ?? new List<CourseAssignmentViewModel>())).Remove(courseToRemove);
                        }
                    }
                }
            }
        }
    }
}
