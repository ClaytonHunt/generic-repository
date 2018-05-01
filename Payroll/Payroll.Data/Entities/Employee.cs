using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Payroll.Business.interfaces;

namespace Payroll.Data.Entities
{
    public class Employee : IStandardIdentity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(11)]

        public string SocialSecurityNumber { get; set; }

        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public EmployeeType Type { get; set; }

        public int? HourlyPayRateId { get; set; }
        
        public virtual HourlyPayRate HourlyPayRate { get; set; }

        [ForeignKey("SalaryPayRate")]
        public int? SalaryPayRateId { get; set; }

        public virtual SalaryPayRate SalaryPayRate { get; set; }

        public int? CommissionPayRateId { get; set; }

        public virtual CommissionPayRate CommissionPayRate { get; set; }

        public virtual IList<EmployeeHour> EmployeeHours { get; set; }        
        
        public virtual IList<EmployeeSale> EmployeeSales { get; set; }        
    }
}
