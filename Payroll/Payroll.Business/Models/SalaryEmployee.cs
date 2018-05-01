using System;

namespace Payroll.Business.Models
{
    public class SalaryEmployee : Employee
    {
        public decimal Salary { set; get; }

        public decimal PeriodPay => (Salary / 52) * 2;

        public decimal TotalPayToDate
        {
            get
            {
                var currentDate = DateTime.Now; // This is bad, don't use DateTime.Now directly, instead abstract to a service class that can be injected

                var payPeriods = (currentDate.DayOfYear / 7) / 2; // Not quite right, but okay for demo :)

                return PeriodPay * payPeriods;
            }
        }

        public SalaryEmployee()
        {
            Type = EmployeeType.Salary;
        }
    }
}