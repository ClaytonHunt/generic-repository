using System.Linq;
using Microsoft.EntityFrameworkCore;
using Payroll.Business.interfaces;
using EmployeeDto = Payroll.Business.Models.Employee;
using HourlyEmployeeDto = Payroll.Business.Models.HourlyEmployee;
using SalaryEmployeeDto = Payroll.Business.Models.SalaryEmployee;
using CommissionEmployeeDto = Payroll.Business.Models.CommissionEmployee;
using EmployeeEntity = Payroll.Data.Entities.Employee;
using EmployeeType = Payroll.Data.Entities.EmployeeType;

namespace Payroll.Data.Mappers
{
    public class EmployeeMapper : IMapper<EmployeeDto, EmployeeEntity>
    {
        private readonly IMapper<HourlyEmployeeDto, EmployeeEntity> _hourlyMapper;
        private readonly IMapper<SalaryEmployeeDto, EmployeeEntity> _salaryMapper;
        private readonly IMapper<CommissionEmployeeDto, EmployeeEntity> _commissionMapper;

        public EmployeeMapper(IMapper<HourlyEmployeeDto, EmployeeEntity> hourlyMapper,
                              IMapper<SalaryEmployeeDto, EmployeeEntity> salaryMapper,
                              IMapper<CommissionEmployeeDto, EmployeeEntity> commissionMapper)
        {
            _hourlyMapper = hourlyMapper;
            _salaryMapper = salaryMapper;
            _commissionMapper = commissionMapper;
        }

        public EmployeeDto SingleTo(EmployeeEntity item)
        {
            switch (item.Type)
            {
                case EmployeeType.Salary:
                    return _salaryMapper.SingleTo(item);
                case EmployeeType.Commission:
                    return _commissionMapper.SingleTo(item);
                default:
                    return _hourlyMapper.SingleTo(item);
            }
        }

        public IQueryable<EmployeeDto> ManyTo(IQueryable<EmployeeEntity> items)
        {
            return items.Select(item => item.Type == EmployeeType.Salary ?
                    _salaryMapper.SingleTo(item) :
                    (item.Type == EmployeeType.Commission ?
                        (EmployeeDto)_commissionMapper.SingleTo(item) :
                        _hourlyMapper.SingleTo(item)));
        }

        public EmployeeEntity SingleFrom(EmployeeDto item)
        {
            EmployeeType type;

            switch (item.Type)
            {
                case Business.Models.EmployeeType.Salary:
                    type = EmployeeType.Salary;
                    break;
                case Business.Models.EmployeeType.Commission:
                    type = EmployeeType.Commission;
                    break;
                default:
                    type = EmployeeType.Hourly;
                    break;
            }

            return new EmployeeEntity
            {
                Id = item.Id,
                SocialSecurityNumber = item.SSN,
                FirstName = item.FirstName,
                LastName = item.LastName,
                Type = type
            };
        }

        public IQueryable<EmployeeEntity> WithIncludes(IQueryable<EmployeeEntity> items)
        {
            return _hourlyMapper.WithIncludes(_salaryMapper.WithIncludes(_commissionMapper.WithIncludes(items)));

        }
    }
}
