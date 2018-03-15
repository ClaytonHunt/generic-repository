using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Data;
using ContosoUniversity.Models;

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
        public Department Department { get; set; }
        public SelectList InstructorNames { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Department = await _context.Departments
                .Include(d => d.Administrator)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.DepartmentId == id);

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
                return await HandleDeletedDepartment();
            }

            // Update the RowVersion to the value when this entity was
            // fetched. If the entity has been updated after it was
            // fetched, RowVersion won't matchthe DB RowVersion and
            // a DbUpdateConcurrencyException is thrown.
            // A second postback will make them match, unless a new
            // concurrency issue happens.
            _context.Entry(departmentToUpdate)
                .Property("RowVersion").OriginalValue = Department.RowVersion;

            if (await TryUpdateModelAsync(
                departmentToUpdate,
                "Department",
                d => d.Name,
                d => d.StartDate,
                d => d.Budget,
                d => d.InstructorId))
            {
                try
                {
                    await _context.SaveChangesAsync();

                    return RedirectToPage("./Index");
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var exceptionEntry = ex.Entries.Single();
                    var clientValues = (Department) exceptionEntry.Entity;
                    var databaseEntry = exceptionEntry.GetDatabaseValues();

                    if (databaseEntry == null)
                    {
                        ModelState.AddModelError(String.Empty, "Unable to save. " +
                                                               "The department was deleted by another user.");

                        return Page();
                    }

                    var dbValues = (Department) databaseEntry.ToObject();
                    await SetDbErrorMesssage(dbValues, clientValues, _context);

                    // Save the current RowVersion so next postback
                    // matches unless a new concurrency issue happens.
                    Department.RowVersion = (byte[]) dbValues.RowVersion;
                    // Must clear the model error for the next postback
                    ModelState.Remove("Department.RowVersion");
                }
            }

            InstructorNames = new SelectList(
                _context.Instructors, 
                "Id", 
                "FullName", 
                departmentToUpdate.InstructorId
            );

            return Page();
        }

        private async Task<IActionResult> HandleDeletedDepartment()
        {
            Department deleteDepartment = new Department();

            // ModelState contains the posted data because of the deletion
            // error and will override the Department instance values when displaying the Page().
            ModelState.AddModelError(string.Empty, "Unable to save. The department was deleted by another user.");
            InstructorNames = new SelectList(_context.Instructors, "Id", "FullName", Department.InstructorId);

            return Page();
        }

        private async Task SetDbErrorMesssage(Department dbValues, Department clientValues, SchoolContext context)
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
                    Instructor dbInstructor = await _context.Instructors
                        .FindAsync(dbValues.InstructorId);
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
