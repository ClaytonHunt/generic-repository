using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GenericRepository
{
    public interface IStandardIdentity
    {
        int Id { get; }
    }
}