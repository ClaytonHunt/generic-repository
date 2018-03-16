using ContosoUniversity.Models.SchoolViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ContosoUniversity.Pages.Instructors
{
    public class InstructorCoursesPageModel : PageModel
    {

        public List<AssignedCourseData> AssignedCourseDataList;

        public void PopulateAssignedCourseData(IEnumerable<CourseViewModel> allCourses,
                                               InstructorViewModel instructor)
        {            
            var instructorCourses = new HashSet<int>(instructor.CourseAssignments.Select(c => c.Course.CourseId));
            AssignedCourseDataList = new List<AssignedCourseData>();

            foreach (var course in allCourses)
            {
                AssignedCourseDataList.Add(new AssignedCourseData
                {
                    CourseId = course.CourseId,
                    Title = course.Title,
                    Assigned = instructorCourses.Contains(course.CourseId)
                });
            }
        }

        public void UpdateInstructorCourses(IQueryable<CourseViewModel> allCourses,
            string[] selectedCourses, InstructorViewModel instructorToUpdate)
        {
            if (selectedCourses == null)
            {
                instructorToUpdate.CourseAssignments = new List<CourseAssignmentViewModel>();
                return;
            }

            var selectedCoursesHS = new HashSet<string>(selectedCourses);
            var instructorCourses = new HashSet<int>(instructorToUpdate.CourseAssignments.Select(c => c.Course.CourseId));

            var a = instructorToUpdate.CourseAssignments.Count();

            foreach (var course in allCourses)
            {
                if (selectedCoursesHS.Contains(course.CourseId.ToString()))
                {
                    if (!instructorCourses.Contains(course.CourseId))
                    {
                        instructorToUpdate.CourseAssignments = instructorToUpdate.CourseAssignments.ToList();
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
                        var courseToRemove = instructorToUpdate.CourseAssignments.FirstOrDefault(c => c.Course.CourseId == course.CourseId);

                        if (courseToRemove != null)
                        {
                            ((IList<CourseAssignmentViewModel>)instructorToUpdate.CourseAssignments).Remove(courseToRemove);
                        }                        
                    }
                }
            }
        }
    }
}