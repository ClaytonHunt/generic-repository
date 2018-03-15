using ContosoUniversity.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ContosoUniversity.Pages.Courses
{
    public class DepartmentNamePageModel : PageModel
    {
        public SelectList DepartmentName { get; set; }

        public void PopulateDepartmentsDropDownList(SchoolContext context,
            object selectedDepartment = null)
        {
            var departmentsQuery = from d in context.Departments
                orderby d.Name // Sort by name.
                select d;

            DepartmentName = new SelectList(departmentsQuery.AsNoTracking(),
                "DepartmentId", "Name", selectedDepartment);
        }
    }
}