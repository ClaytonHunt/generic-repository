using System;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Models;
using ContosoUniversity.Models.SchoolViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Pages.Departments
{
    public class EditModel : PageModel
    {
        private readonly ContosoUniversity.Data.SchoolContext _context;

        public EditModel(ContosoUniversity.Data.SchoolContext context)
        {
            _context = context;
        }

        [BindProperty]
        public DepartmentViewModel Department { get; set; }
        public SelectList InstructorNames { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Department = await _context.Departments.Select(d => new DepartmentViewModel
            {
                Id = d.DepartmentId,
                Version = d.RowVersion[7],
                Name = d.Name,
                Budget = d.Budget,
                StartDate = d.StartDate,
                Administrator = d.Administrator == null ? null : new InstructorViewModel
                {
                    Id = d.Administrator.Id,
                    FirstMidName = d.Administrator.FirstMidName,
                    LastName = d.Administrator.LastName,
                    HireDate = d.Administrator.HireDate,
                    OfficeAssignment = d.Administrator.OfficeAssignment == null ? null : new OfficeAssignmentViewModel
                    {
                        Location = d.Administrator.OfficeAssignment.Location
                    },
                    CourseAssignments = d.Administrator.CourseAssignments == null ? null : d.Administrator.CourseAssignments.Select(ca => new CourseAssignmentViewModel
                    {
                        Course = ca.Course == null ? null : new CourseViewModel
                        {
                            CourseId = ca.Course.CourseId,
                            Title = ca.Course.Title,
                            Credits = ca.Course.Credits,
                            Department = ca.Course.Department == null ? null : new DepartmentViewModel
                            {
                                Name = ca.Course.Department.Name
                            },
                            Enrollments = ca.Course.Department == null ? null : ca.Course.Enrollments.Select(e => new EnrollmentViewModel
                            {
                                Grade = e.Grade,
                                CourseTitle = ca.Course.Title,
                                StudentName = e.Student.FullName
                            })
                        }
                    })
                }
            })
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Department == null)
            {
                return NotFound();
            }

            InstructorNames = new SelectList(_context.Instructors, "Id", "FirstMidName");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var departmentToUpdate = await _context.Departments
                .Include(d => d.Administrator)
                .FirstOrDefaultAsync(d => d.DepartmentId == id);

            // null means Department was deleted by another user.
            if (departmentToUpdate == null)
            {
                return HandleDeletedDepartment();
            }

            // Update the RowVersion to the value when this entity was
            // fetched. If the entity has been updated after it was
            // fetched, RowVersion won't matchthe DB RowVersion and
            // a DbUpdateConcurrencyException is thrown.
            // A second postback will make them match, unless a new
            // concurrency issue happens.
            ((byte[])_context.Entry(departmentToUpdate).Property("RowVersion").OriginalValue)[7] = (byte)Department.Version;

            departmentToUpdate.Name = Department.Name;
            departmentToUpdate.StartDate = Department.StartDate;
            departmentToUpdate.Budget = Department.Budget;
            departmentToUpdate.InstructorId = Department.Administrator.Id;

            try
            {
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var exceptionEntry = ex.Entries.Single();
                var clientValues = (Department)exceptionEntry.Entity;
                var databaseEntry = exceptionEntry.GetDatabaseValues();

                if (databaseEntry == null)
                {
                    ModelState.AddModelError(string.Empty, "Unable to save. " +
                                                           "The department was deleted by another user.");

                    return Page();
                }

                var dbValues = (Department)databaseEntry.ToObject();
                await SetDbErrorMesssage(dbValues, clientValues);

                // Save the current RowVersion so next postback
                // matches unless a new concurrency issue happens.
                Department.Version = dbValues.RowVersion[7];
                // Must clear the model error for the next postback
                ModelState.Remove("Department.RowVersion");
            }

            InstructorNames = new SelectList(
                _context.Instructors,
                "Id",
                "FullName",
                departmentToUpdate.InstructorId
            );

            return Page();
        }

        private IActionResult HandleDeletedDepartment()
        {
            // ModelState contains the posted data because of the deletion
            // error and will override the Department instance values when displaying the Page().
            ModelState.AddModelError(string.Empty, "Unable to save. The department was deleted by another user.");
            InstructorNames = new SelectList(_context.Instructors, "Id", "FullName", Department.Administrator.Id);

            return Page();
        }

        private async Task SetDbErrorMesssage(Department dbValues, Department clientValues)
        {
            if (dbValues.Name != clientValues.Name)
            {
                if (dbValues.Name != clientValues.Name)
                {
                    ModelState.AddModelError("Department.Name",
                        $"Current value: {dbValues.Name}");
                }
                if (dbValues.Budget != clientValues.Budget)
                {
                    ModelState.AddModelError("Department.Budget",
                        $"Current value: {dbValues.Budget:c}");
                }
                if (dbValues.StartDate != clientValues.StartDate)
                {
                    ModelState.AddModelError("Department.StartDate",
                        $"Current value: {dbValues.StartDate:d}");
                }
                if (dbValues.InstructorId != clientValues.InstructorId)
                {
                    var dbInstructor = await _context.Instructors.Select(i => new InstructorViewModel
                    {
                        Id = i.Id,
                        FirstMidName = i.FirstMidName,
                        LastName = i.LastName,
                        HireDate = i.HireDate,
                        OfficeAssignment = i.OfficeAssignment == null ? null : new OfficeAssignmentViewModel
                        {
                            Location = i.OfficeAssignment.Location
                        },
                        CourseAssignments = i.CourseAssignments == null ? null : i.CourseAssignments.Select(ca => new CourseAssignmentViewModel
                        {
                            Course = ca.Course == null ? null : new CourseViewModel
                            {
                                CourseId = ca.Course.CourseId,
                                Title = ca.Course.Title,
                                Credits = ca.Course.Credits,
                                Department = ca.Course.Department == null ? null : new DepartmentViewModel
                                {
                                    Name = ca.Course.Department.Name
                                },
                                Enrollments = ca.Course.Enrollments == null ? null : ca.Course.Enrollments.Select(e => new EnrollmentViewModel
                                {
                                    Grade = e.Grade,
                                    CourseTitle = ca.Course.Title,
                                    StudentName = e.Student.FullName
                                })
                            }
                        })
                    })
                        .FirstOrDefaultAsync(i => i.Id == dbValues.InstructorId);
                    ModelState.AddModelError("Department.InstructorID",
                        $"Current value: {dbInstructor?.FullName}");
                }

                ModelState.AddModelError(string.Empty,
                    "The record you attempted to edit "
                    + "was modified by another user after you. The "
                    + "edit operation was canceled and the current values in the database "
                    + "have been displayed. If you still want to edit this record, click "
                    + "the Save button again.");
            }
        }
    }
}
