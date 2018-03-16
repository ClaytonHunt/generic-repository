using ContosoUniversity.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ContosoUniversity.Models.SchoolViewModels;

namespace ContosoUniversity.Pages.Courses
{
    public class DepartmentNamePageModel : PageModel
    {
        public SelectList DepartmentName { get; set; }

        public void PopulateDepartmentsDropDownList(SchoolContext context,
            object selectedDepartment = null)
        {
            var departmentsQuery = context.Departments.Select(d => new DepartmentViewModel
            {
                Id = d.DepartmentId,
                Name = d.Name
            }).OrderBy(d => d.Name);
                

            DepartmentName = new SelectList(departmentsQuery,
                "Id", "Name", selectedDepartment);
        }
    }
}