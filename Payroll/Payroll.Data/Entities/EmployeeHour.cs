using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Payroll.Data.Entities
{
    public class EmployeeHour
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("EmployeeSSN")]
        public virtual Employee Employee { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime Date { get; set; }

        public float Hours { get; set; }
    }
}