using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Payroll.Business.interfaces;
using Payroll.Business.Models;
using Payroll.Data.Entities;
using Employee = Payroll.Data.Entities.Employee;
using EmployeeType = Payroll.Data.Entities.EmployeeType;

namespace Payroll.Data.Mappers
{
    public class CommissionEmployeeMapper : IMapper<CommissionEmployee, Employee>
    {
        public CommissionEmployee SingleTo(Employee item)
        {
            return new CommissionEmployee
            {
                Id = item.Id,
                SSN = item.SocialSecurityNumber,
                FirstName = item.FirstName,
                LastName = item.LastName,
                Salary = item.SalaryPayRate == null ? 0 : item.SalaryPayRate.AnnualRate,
                Commission = item.CommissionPayRate == null ? 0 : item.CommissionPayRate.CommissionRate,
                Sales = (item.EmployeeSales ?? new List<EmployeeSale>()).Select(h => new Sale
                {
                    Date = h.Date,
                    Earnings = h.Amount
                })
            };
        }

        public IQueryable<CommissionEmployee> ManyTo(IQueryable<Employee> items)
        {
            return items.Select(item => new CommissionEmployee
            {
                Id = item.Id,
                SSN = item.SocialSecurityNumber,
                FirstName = item.FirstName,
                LastName = item.LastName,
                Salary = item.SalaryPayRate == null ? 0 : item.SalaryPayRate.AnnualRate,
                Commission = item.CommissionPayRate == null ? 0 : item.CommissionPayRate.CommissionRate,
                Sales = (item.EmployeeSales ?? new List<EmployeeSale>()).Select(h => new Sale
                {
                    Date = h.Date,
                    Earnings = h.Amount
                })
            });
        }

        public Employee SingleFrom(CommissionEmployee item)
        {
            // Unable to update Commission Rate and Sales using this mapper
            // Points out a leak in the abstraction
            return new Employee
            {
                Id = item.Id,
                SocialSecurityNumber = item.SSN,
                FirstName = item.FirstName,
                LastName = item.LastName,
                Type = EmployeeType.Commission
            };
        }

        public IQueryable<Employee> WithIncludes(IQueryable<Employee> items)
        {
            return items.Include(x => x.SalaryPayRate)
                .Include(x => x.CommissionPayRate)
                .Include(x => x.EmployeeSales);
        }
    }
}