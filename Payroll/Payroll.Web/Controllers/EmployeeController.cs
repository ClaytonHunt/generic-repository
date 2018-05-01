using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payroll.Business;
using Payroll.Business.Models;
using Payroll.Data;
using Payroll.Data.Entities;
using Employee = Payroll.Business.Models.Employee;
using EmployeeType = Payroll.Business.Models.EmployeeType;

namespace Payroll.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly PayrollContext _context;
        private readonly EmployeeService _service;

        public EmployeeController(PayrollContext context, EmployeeService service)
        {
            _context = context;
            _service = service;
        }

        public IActionResult Index()
        {
            return View(new Employee());
        }

        public async Task<IActionResult> Create(Employee employee)
        {
            await _service.Create(employee);

            return Redirect("/");
        }

        public async Task<IActionResult> View(int id)
        {
            var employees = _context.Employees
                .Include(x => x.EmployeeHours)
                .Include(x => x.EmployeeSales)
                .Include(x => x.HourlyPayRate)
                .Include(x => x.SalaryPayRate)
                .Include(x => x.CommissionPayRate);

            var employee = await employees.FirstOrDefaultAsync(x => x.Id == id);

            if (employee == null)
            {
                return NotFound(id);
            }

            switch (employee.Type)
            {
                case Data.Entities.EmployeeType.Salary:
                    return View("SalaryReport", await employees.Where(x => x.Id == id).Select(x => new SalaryEmployee
                    {
                        Id = x.Id,
                        SSN = x.SocialSecurityNumber,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        Salary = x.SalaryPayRate == null ? 0 : x.SalaryPayRate.AnnualRate
                    }).FirstAsync());
                case Data.Entities.EmployeeType.Commission:
                    return View("CommissionReport", await employees.Where(x => x.Id == id).Select(x => new CommissionEmployee
                    {
                        Id = x.Id,
                        SSN = x.SocialSecurityNumber,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        Salary = x.SalaryPayRate == null ? 0 : x.SalaryPayRate.AnnualRate,
                        Commission = x.CommissionPayRate == null ? 0 : x.CommissionPayRate.CommissionRate,
                        Sales = x.EmployeeSales == null ? new List<Sale>().AsEnumerable() : x.EmployeeSales.Select(y => new Sale
                        {
                            Date = y.Date,
                            Earnings = y.Amount
                        })
                    }).FirstAsync());
                case Data.Entities.EmployeeType.Hourly:
                    return View("HourlyReport", await employees.Where(x => x.Id == id).Select(x => new HourlyEmployee
                    {
                        Id = x.Id,
                        SSN = x.SocialSecurityNumber,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        Rate = x.HourlyPayRate == null ? 0 : x.HourlyPayRate.Rate,
                        TimeEntries = x.EmployeeHours == null ? new List<TimeEntry>().AsEnumerable()  : x.EmployeeHours.Select(y => new TimeEntry {
                            Date = y.Date,
                            Hours = y.Hours
                        })
                    }).FirstAsync());
                default:
                    return NotFound(id);
            }

            //var employee = await _service.GetById(id);

            //if (employee == null)
            //{
            //    return NotFound(id);
            //}

            //switch (employee.Type)
            //{
            //    case EmployeeType.Salary:
            //        return View("SalaryReport", employee);
            //    case EmployeeType.Commission:
            //        return View("CommissionReport", employee);
            //    case EmployeeType.Hourly:
            //        return View("HourlyReport", employee);
            //    default:
            //        return NotFound(id);

            //}
        }
    }
}