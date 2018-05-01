using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Payroll.Business;
using Payroll.Data;
using Payroll.Data.Entities;
using Payroll.Web.Models;

namespace Payroll.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly PayrollContext _context;
        private readonly EmployeeService _service;

        public HomeController(PayrollContext context, EmployeeService service)
        {
            _context = context;
            _service = service;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var employees = _context.Employees.ToList().Select(e => new SimpleEmployeeViewModel
            {
                Id = e.Id,
                SSN = e.SocialSecurityNumber,
                Name = $"{e.FirstName} {e.LastName}",
                Type = e.Type.ToString()
            });

            return View(employees);

            //var employees = await _service.GetAll(page);

            //return View(employees.Select(e => new SimpleEmployeeViewModel
            //{
            //    Id = e.Id,
            //    SSN = e.SSN,
            //    Name = $"{e.FirstName} {e.LastName}",
            //    Type = e.Type.ToString()
            //}));
        }        

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
