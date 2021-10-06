
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vetappback.DTOs;

namespace vetappback.Utilities
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, PaginationsDTO paginationsDTO)
        {
            return queryable
                .Skip((paginationsDTO.Page - 1) * paginationsDTO.RecordsbyPage)
                .Take(paginationsDTO.RecordsbyPage);
        }
        
    }
}