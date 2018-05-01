using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Payroll.Data;
using Payroll.Data.Entities;
using Payroll.Web.Models;

namespace Payroll.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly PayrollContext _context;

        public HomeController(PayrollContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var employees = _context.Employees.ToList().Select(e => new SimpleEmployeeViewModel {
                Id = e.Id,
                SSN = e.SocialSecurityNumber,
                Name = $"{e.FirstName} {e.LastName}",
                Type = ((EmployeeType)e.Type).ToString()
            });

            return View(employees);
        }        

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
