using System.Threading.Tasks;
using Payroll.Business.interfaces;
using Payroll.Business.Models;

namespace Payroll.Business
{
    public class EmployeeService
    {
        private readonly IRepository<Employee> _employeeRepo;

        public EmployeeService(IRepository<Employee> employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }

        public async Task<Employee> Create(Employee employee)
        {
            var emp = await _employeeRepo.CreateAsync(employee);

            return emp;
        }

        public async Task<Employee> GetById(int id)
        {
            var emp = await _employeeRepo.GetByIdAsync(id);

            return emp;
        }

        public async Task<IPaginatedList<Employee>> GetAll(int page)
        {
            return await _employeeRepo.GetAllPagedAsync((e) => e, page, 10);
        }
    }
}
