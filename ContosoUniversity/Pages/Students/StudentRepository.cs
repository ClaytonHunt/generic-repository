using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Data;
using ContosoUniversity.Models.SchoolViewModels;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Pages.Students
{
    public class StudentRepository
    {
        private readonly SchoolContext _context;
        private readonly StudentMapper _mapper;

        public StudentRepository(SchoolContext context, StudentMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<StudentViewModel> CreateAsync(StudentViewModel student)
        {
            var newStudent = _context.Add(_mapper.SingleFrom(student));

            await _context.SaveChangesAsync();

            return _mapper.SingleTo(newStudent.Entity);
        }

        public async Task<StudentViewModel> GetByIdAsync(int id)
        {
            return await _mapper.ManyTo(_context.Students).FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<StudentViewModel>> GetAllAsync(
            Func<IQueryable<StudentViewModel>, IQueryable<StudentViewModel>> filterFunc)
        {
            filterFunc = filterFunc ?? (s => s);

            return await filterFunc(_mapper.ManyTo(_context.Students)).ToListAsync();
        }

        public async Task<PaginatedList<StudentViewModel>> GetAllPagedAsync(Func<IQueryable<StudentViewModel>, IQueryable<StudentViewModel>> filterFunc, int pageIndex, int pageSize)
        {
            filterFunc = filterFunc ?? (s => s);

            var students = filterFunc(_mapper.ManyTo(_context.Students));

            return await PaginatedList< StudentViewModel >.CreateAsync(students, pageIndex, pageSize);
        }

        public async Task<StudentViewModel> UpdateAsync(StudentViewModel student)
        {
            var updatedStudent = _context.Students.Update(_mapper.SingleFrom(student));

            await _context.SaveChangesAsync();

            return _mapper.SingleTo(updatedStudent.Entity);
        }

        public async Task<IEnumerable<StudentViewModel>> UpdateManyAsync(IEnumerable<StudentViewModel> students)
        {
            var studentsToUpdate = students.ToList();

            _context.Students.UpdateRange(studentsToUpdate.Select(_mapper.SingleFrom));

            await _context.SaveChangesAsync();

            return studentsToUpdate;
        }

        public async Task DeleteAsync(StudentViewModel student)
        {
            _context.Students.Remove(_mapper.SingleFrom(student));

            await _context.SaveChangesAsync();
        }

        public async Task DeleteManyAsnyc(IEnumerable<StudentViewModel> students)
        {
            _context.Students.RemoveRange(students.Select(_mapper.SingleFrom));

            await _context.SaveChangesAsync();
        }
    }
}