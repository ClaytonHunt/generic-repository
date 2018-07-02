using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GenericRepository
{
    public interface IRepository<T>
    {
        T Create(T item);
        T GetById(int id);
        IQueryable<T> GetAll();
        T Update(T item);
        void Delete(T item);
    }
}