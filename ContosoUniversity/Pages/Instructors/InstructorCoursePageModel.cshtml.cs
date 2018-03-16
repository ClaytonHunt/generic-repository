using ContosoUniversity.Data;
using ContosoUniversity.Models.SchoolViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using ContosoUniversity.Models;
using Microsoft.EntityFrameworkCore;

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

        public async void UpdateInstructorCourses(SchoolContext context,
            string[] selectedCourses, Instructor instructorToUpdate)
        {
            if (selectedCourses == null)
            {
                instructorToUpdate.CourseAssignments = new List<CourseAssignment>();
                return;
            }

            var selectedCoursesHS = new HashSet<string>(selectedCourses);
            var instructorCourses = new HashSet<int>
                (instructorToUpdate.CourseAssignments.Select(c => c.CourseId));

            foreach (var course in context.Courses.Select(c => new CourseViewModel
            {
                CourseId = c.CourseId,
                Title = c.Title,
                Enrollments = c.Enrollments.Select(e => new EnrollmentViewModel
                {
                    Id = e.EnrollmentId,
                    Grade = e.Grade,
                    StudentName = e.Student.FullName,
                    CourseTitle = e.Course.Title
                }),
                Credits = c.Credits,
                Department = new DepartmentViewModel
                {
                    Id = c.Department.DepartmentId,
                    Name = c.Department.Name
                },
            }))
            {
                if (selectedCoursesHS.Contains(course.CourseId.ToString()))
                {
                    if (!instructorCourses.Contains(course.CourseId))
                    {
                        instructorToUpdate.CourseAssignments = instructorToUpdate.CourseAssignments.ToList();
                        ((IList<CourseAssignment>)instructorToUpdate.CourseAssignments).Add(new CourseAssignment
                        {
                            Instructor = instructorToUpdate,
                            CourseId = course.CourseId
                        });
                    }
                }
                else
                {
                    if (instructorCourses.Contains(course.CourseId))
                    {
                        var courseToRemove = await context.Courses.FirstOrDefaultAsync(c => c.CourseId == course.CourseId);

                        if (courseToRemove != null)
                        {
                            context.Remove(courseToRemove);
                        }                        
                    }
                }
            }
        }
    }
}