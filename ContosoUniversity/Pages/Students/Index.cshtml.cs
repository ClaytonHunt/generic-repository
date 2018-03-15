using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Data;
using ContosoUniversity.Models.SchoolViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly SchoolContext _context;

        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public IndexModel(SchoolContext context)
        {
            _context = context;
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

            var studentIQ = from s in _context.Students
                            select new StudentViewModel
                            {
                                Id = s.Id,
                                LastName = s.LastName,
                                FirstMidName = s.FirstMidName,
                                EnrollmentDate = s.EnrollmentDate
                            };

            if (!string.IsNullOrEmpty(searchString))
            {
                studentIQ = studentIQ.Where(s =>
                    s.LastName.Contains(searchString) || s.FirstMidName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    studentIQ = studentIQ.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    studentIQ = studentIQ.OrderBy(s => s.EnrollmentDate);
                    break;
                case "date_desc":
                    studentIQ = studentIQ.OrderByDescending(s => s.EnrollmentDate);
                    break;
                default:
                    studentIQ = studentIQ.OrderBy(s => s.LastName);
                    break;
            }

            const int pageSize = 3;

            Student = await PaginatedList<StudentViewModel>.CreateAsync(studentIQ.AsNoTracking(), pageIndex, pageSize);
        }
    }
}
