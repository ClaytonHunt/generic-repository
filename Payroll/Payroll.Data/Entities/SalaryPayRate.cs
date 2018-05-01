using System.ComponentModel.DataAnnotations.Schema;

namespace Payroll.Data.Entities
{
    public class SalaryPayRate
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "Money")]
        public decimal AnnualRate { get; set; }
    }
}