using System;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Data;
using ContosoUniversity.Models.SchoolViewModels;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Pages.Students
{
    public class StudentService
    {
        private readonly SchoolContext _context;

        public StudentService(SchoolContext context)
        {
            _context = context;
        }
        public async Task<PaginatedList<StudentViewModel>> GetAllPagedAsync(string searchString = null, Func<IQueryable<StudentViewModel>, IQueryable<StudentViewModel>> orderBy = null, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            orderBy = orderBy ?? (s => s);

            return await PaginatedList<StudentViewModel>.CreateAsync(orderBy(new StudentMapper().ManyTo(
                _context.Students.Where(s => string.IsNullOrEmpty(searchString) || 
                                             s.LastName.Contains(searchString) || 
                                             s.FirstMidName.Contains(searchString)))), pageIndex, pageSize);
        }

        public async Task<StudentViewModel> GetByIdAsync(int id)
        {
            return await new StudentMapper().ManyTo(_context.Students).FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task UpdateAsync(StudentViewModel student)
        {
            _context.Students.Update(new StudentMapper().SingleFrom(student));

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(StudentViewModel student)
        {
            _context.Students.Remove(new StudentMapper().SingleFrom(student));

            await _context.SaveChangesAsync();
        }

        public async Task CreateAsync(StudentViewModel student)
        {
            _context.Add(new StudentMapper().SingleFrom(student));

            await _context.SaveChangesAsync();
        }
    }
}
