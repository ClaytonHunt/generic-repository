using System.Linq;
using Microsoft.EntityFrameworkCore;
using Payroll.Business.interfaces;
using Payroll.Business.Models;
using Employee = Payroll.Data.Entities.Employee;
using EmployeeType = Payroll.Data.Entities.EmployeeType;

namespace Payroll.Data.Mappers
{
    public class SalaryEmployeeMapper : IMapper<SalaryEmployee, Employee>
    {
        public SalaryEmployee SingleTo(Employee item)
        {
            return new SalaryEmployee
            {
                Id = item.Id,
                SSN = item.SocialSecurityNumber,
                FirstName = item.FirstName,
                LastName = item.LastName,
                Salary = item.SalaryPayRate == null ? 0 : item.SalaryPayRate.AnnualRate,                
            };
        }

        public IQueryable<SalaryEmployee> ManyTo(IQueryable<Employee> items)
        {
            return items.Include(x => x.SalaryPayRate)
                        .Select(item => new SalaryEmployee {
                    Id = item.Id,
                SSN = item.SocialSecurityNumber,
                FirstName = item.FirstName,
                LastName = item.LastName,
                Salary = item.SalaryPayRate == null ? 0 : item.SalaryPayRate.AnnualRate,                
            });
        }

        public Employee SingleFrom(SalaryEmployee item)
        {
            // Unable to update Salary Rate using this mapper
            // Points out a leak in the abstraction
            return new Employee
            {
                Id = item.Id,
                SocialSecurityNumber = item.SSN,
                FirstName = item.FirstName,
                LastName = item.LastName,
                Type = EmployeeType.Salary
            };
        }

        public IQueryable<Employee> WithIncludes(IQueryable<Employee> items)
        {
            return items.Include(x => x.SalaryPayRate);
        }
    }
}