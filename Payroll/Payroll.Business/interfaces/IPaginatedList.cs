using System.Collections.Generic;

namespace Payroll.Business.interfaces
{
    public interface IPaginatedList<T> : IList<T>
    {
        int PageIndex { get; }
        int TotalPages { get; }
        bool HasPreviousPage { get; }
        bool HasNextPage { get; } 
    }
}