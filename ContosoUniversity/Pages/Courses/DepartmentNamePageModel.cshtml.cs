using ContosoUniversity.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using ContosoUniversity.Pages.Instructors;

namespace ContosoUniversity.Pages.Courses
{
    public class DepartmentNamePageModel : PageModel
    {
        protected readonly SchoolContext Context;

        public DepartmentNamePageModel(SchoolContext context)
        {
            Context = context;
        }

        public SelectList DepartmentName { get; set; }

        public void PopulateDepartmentsDropDownList(object selectedDepartment = null)
        {
            var departmentsQuery = new DepartmentMapper().ManyTo(Context.Departments).OrderBy(d => d.Name);

            DepartmentName = new SelectList(departmentsQuery, "Id", "Name", selectedDepartment);
        }
    }
}