using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Payroll.Data.Entities
{
    public class EmployeeSale
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("EmployeeSSN")]
        public virtual Employee Employee { get; set; }

        public DateTime Date { get;set; }
        public decimal Amount { get; set; }
    }
}