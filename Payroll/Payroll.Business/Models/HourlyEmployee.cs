using System;
using System.Collections.Generic;
using System.Linq;

namespace Payroll.Business.Models
{
    public class HourlyEmployee : Employee
    {
        public decimal Rate { get; set; }        

        public decimal TotalPayToDate => Rate * (decimal) TimeEntries.Select(x => x.Hours).Aggregate((a, b) => a + b);

        public IEnumerable<TimeEntry> TimeEntries { get; set; }

        public HourlyEmployee()
        {
            Type = EmployeeType.Hourly;
        }
    }
}