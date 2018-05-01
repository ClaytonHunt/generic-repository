using System;
using System.Collections.Generic;
using System.Linq;

namespace Payroll.Business.Models
{
    public class CommissionEmployee : Employee
    {
        public decimal Salary { get; set; }

        public decimal PeriodPay => (Salary / 52) * 2;

        public decimal CommissionPay => Sales.Select(x => x.Earnings).Aggregate((a, b) => a + b) * (decimal) Commission;

        public decimal TotalPayToDate
        {
            get
            {
                var currentDate = DateTime.Now; // This is bad, don't use DateTime.Now directly, instead abstract to a service class that can be injected

                var payPeriods = (currentDate.DayOfYear / 7) / 2; // Not quite right, but okay for demo :)

                return (PeriodPay * payPeriods) + CommissionPay;
            }
        }

        public float Commission { get; set; }

        public IEnumerable<Sale> Sales { get; set; }

        public CommissionEmployee()
        {
            Type = EmployeeType.Commission;
        }
    }
}