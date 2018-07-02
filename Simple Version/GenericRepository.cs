using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GenericRepository
{
    public class GenericRepository<T> : IRepository<T> where T : class where T : IStandardIdentity
    {
        private readonly DbContext _context;
        
        private IQueryable<T> Data => _context.Set<T>();

        public GenericRepository(DbContext context)
        {
            _context = context;
        }

        public T Create(T item)
        {
            var newItem = _context.Add(item);

            _context.SaveChanges();

            return newItem.Entity;
        }

        public T GetById(int id)
        {
            return Data.Find(id);
        }

        public IQueryable<T> GetAll()
        {
            return Data;
        }

        public T Update(T item)
        {
            var updatedItem = _context.Update(item);

            _context.SaveChanges();

            return updatedItem.Entity;
        }

        public void Delete(T item)
        {
            _context.Remove(item);

            _context.SaveChanges();
        }        
    }
}