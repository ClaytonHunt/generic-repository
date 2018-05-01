using Payroll.Business.interfaces;

namespace Payroll.Business.Models
{
    public class Employee : IStandardIdentity
    {
        public int Id { get; set; }
        public string SSN { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public EmployeeType Type { get; set; }
    }
}
