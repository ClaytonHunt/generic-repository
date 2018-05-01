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
    public class HourlyEmployeeMapper : IMapper<HourlyEmployee, Employee>
    {
        public HourlyEmployee SingleTo(Employee item)
        {
            return new HourlyEmployee
            {
                Id = item.Id,
                SSN = item.SocialSecurityNumber,
                FirstName = item.FirstName,
                LastName = item.LastName,
                Rate = item.HourlyPayRate == null ? 0 : item.HourlyPayRate.Rate,
                TimeEntries = (item.EmployeeHours ?? new List<EmployeeHour>()).Select(h => new TimeEntry
                {
                    Date = h.Date,
                    Hours = h.Hours
                })
            };
        }

        public IQueryable<HourlyEmployee> ManyTo(IQueryable<Employee> items)
        {
            return items.Select(item => new HourlyEmployee
            {
                Id = item.Id,
                SSN = item.SocialSecurityNumber,
                FirstName = item.FirstName,
                LastName = item.LastName,
                Rate = item.HourlyPayRate == null ? 0 : item.HourlyPayRate.Rate,
                TimeEntries = (item.EmployeeHours ?? new List<EmployeeHour>()).Select(h => new TimeEntry
                {
                    Date = h.Date,
                    Hours = h.Hours
                })
            });
        }

        public Employee SingleFrom(HourlyEmployee item)
        {
            // Unable to update Hourly Rate and Recorded Hours using this mapper
            // Points out a leak in the abstraction
            return new Employee
            {
                Id = item.Id,
                SocialSecurityNumber = item.SSN,
                FirstName = item.FirstName,
                LastName = item.LastName,
                Type = EmployeeType.Hourly
            };
        }

        public IQueryable<Employee> WithIncludes(IQueryable<Employee> items)
        {
            return items.Include(x => x.HourlyPayRate)
                .Include(x => x.EmployeeHours);
        }
    }
}