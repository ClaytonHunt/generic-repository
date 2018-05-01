using System.ComponentModel.DataAnnotations.Schema;

namespace Payroll.Data.Entities
{
    public class HourlyPayRate
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "Money")]
        public decimal Rate { get; set; }
    }
}