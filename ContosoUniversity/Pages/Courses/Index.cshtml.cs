using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Data;
using ContosoUniversity.Models;
using ContosoUniversity.Models.SchoolViewModels;

namespace ContosoUniversity.Pages_Courses
{
    public class IndexModel : PageModel
    {
        private readonly ContosoUniversity.Data.SchoolContext _context;

        public IndexModel(ContosoUniversity.Data.SchoolContext context)
        {
            _context = context;
        }

        public IList<CourseViewModel> CourseVm { get;set; }

        public async Task OnGetAsync()
        {
            CourseVm = await _context.Courses
                .Select(c => new CourseViewModel
                {
                    CourseId = c.CourseId,
                    Title = c.Title,
                    Credits = c.Credits,
                    DepartmentName = c.Department.Name
                })
                .ToListAsync();
        }
    }
}
