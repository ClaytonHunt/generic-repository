using System;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Models.SchoolViewModels;

namespace ContosoUniversity.Pages.Students
{
    public class StudentService
    {
        private readonly IRepository<StudentViewModel> _repository;

        public StudentService(IRepository<StudentViewModel> repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedList<StudentViewModel>> GetAllPagedAsync(string searchString = null,
            Func<IQueryable<StudentViewModel>, IQueryable<StudentViewModel>> orderBy = null, int pageIndex = 0,
            int pageSize = int.MaxValue)
        {
            orderBy = orderBy ?? (s => s);

            IQueryable<StudentViewModel> Filter(IQueryable<StudentViewModel> students)
            {
                return orderBy(
                    students.Where(s => string.IsNullOrEmpty(searchString) || 
                                        s.LastName.Contains(searchString) || 
                                        s.FirstMidName.Contains(searchString)));
            }

            return await _repository.GetAllPagedAsync(Filter, pageIndex, pageSize);
        }

        public async Task<StudentViewModel> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(StudentViewModel student)
        {
            await _repository.UpdateAsync(student);
        }

        public async Task DeleteAsync(StudentViewModel student)
        {
            await _repository.DeleteAsync(student);
        }

        public async Task CreateAsync(StudentViewModel student)
        {
            await _repository.CreateAsync(student);
        }
    }
}
