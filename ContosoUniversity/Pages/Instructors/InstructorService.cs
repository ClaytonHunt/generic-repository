using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Data;
using ContosoUniversity.Models.SchoolViewModels;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Pages.Instructors
{
    public class InstructorService
    {
        private readonly SchoolContext _context;

        public InstructorService(SchoolContext context)
        {
            _context = context;
        }

        public IQueryable<InstructorViewModel> GetAll()
        {
            return new InstructorMapper().ManyTo(_context.Instructors);
        }

        public async Task<InstructorViewModel> GetByIdAsync(int id)
        {
           return await new InstructorMapper().ManyTo(_context.Instructors).FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task UpdateAsync(InstructorViewModel instructorToUpdate)
        {
            _context.Instructors.Update(new InstructorMapper().SingleFrom(instructorToUpdate));

            await _context.SaveChangesAsync();
        }
    }
}
