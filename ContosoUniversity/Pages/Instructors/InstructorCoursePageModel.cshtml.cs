using ContosoUniversity.Data;
using ContosoUniversity.Models.SchoolViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace ContosoUniversity.Pages.Instructors
{
    public class InstructorCoursesPageModel : PageModel
    {

        public List<AssignedCourseData> AssignedCourseDataList;

        public void PopulateAssignedCourseData(SchoolContext context,
                                               InstructorViewModel instructor)
        {
            var allCourses = context.Courses;
            var instructorCourses = new HashSet<int>(
                instructor.CourseAssignments.Select(c => c.Course.CourseId));
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

        public void UpdateInstructorCourses(SchoolContext context,
            string[] selectedCourses, InstructorViewModel instructorToUpdate)
        {
            if (selectedCourses == null)
            {
                instructorToUpdate.CourseAssignments = new List<CourseAssignmentViewModel>();
                return;
            }

            var selectedCoursesHS = new HashSet<string>(selectedCourses);
            var instructorCourses = new HashSet<int>
                (instructorToUpdate.CourseAssignments.Select(c => c.Course.CourseId));

            foreach (var course in context.Courses.Select(c => new CourseViewModel
            {
                CourseId = c.CourseId,
                Title = c.Title,
                Enrollments = c.Enrollments.Select(e => new EnrollmentViewModel
                {
                    Grade = e.Grade,
                    StudentName = e.Student.FullName,
                    CourseTitle = e.Course.Title
                }),
                Credits = c.Credits,
                DepartmentName = c.Department.Name
            }))
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
                        var courseToRemove
                            = instructorToUpdate
                                .CourseAssignments
                                .SingleOrDefault(i => i.Course.CourseId == course.CourseId);

                        context.Remove(courseToRemove);
                    }
                }
            }
        }
    }
}