using System;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Models.SchoolViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContosoUniversity.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly StudentService _service;
        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public IndexModel(StudentService service)
        {
            _service = service;
        }

        public PaginatedList<StudentViewModel> Student { get; set; }

        public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int pageIndex = 1)
        {
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            
            NameSort = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";
            CurrentSort = sortOrder;
            CurrentFilter = searchString;
            const int pageSize = 3;


            Student = await _service.GetAllPagedAsync(
                searchString, OrderedBy(sortOrder), pageIndex, pageSize);
        }

        private Func<IQueryable<StudentViewModel>, IQueryable<StudentViewModel>> OrderedBy(string sortOrder)
        {
            return students =>
            {
                switch (sortOrder)
                {
                    case "name_desc":
                        students = students.OrderByDescending(s => s.LastName);
                        break;
                    case "Date":
                        students = students.OrderBy(s => s.EnrollmentDate);
                        break;
                    case "date_desc":
                        students = students.OrderByDescending(s => s.EnrollmentDate);
                        break;
                    default:
                        students = students.OrderBy(s => s.LastName);
                        break;
                }

                return students;
            };
        }
    }
}
