using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskProject.Core.Wrappers
{
    public  static class QueryableExtensions
    {
        public static async Task<PaginatedResult<T>> ToPaginatedListAsynnc<T>(this IQueryable<T> source,int PageNumber  ,int pageSize ) where T:class
        {
            if (source==null)
            { throw new Exception("Empty"); }
            PageNumber=PageNumber==0 ? 1 : PageNumber;
            pageSize=pageSize==0 ? 10 : pageSize;
            int count = await source.AsNoTracking().CountAsync();
            if (count == 0) return PaginatedResult<T>.success(new List<T>(), count, PageNumber, pageSize);
            PageNumber=PageNumber<=0 ? 1 : PageNumber;
            var items =await source.Skip((PageNumber-1)*pageSize).Take(pageSize).ToListAsync();
            return PaginatedResult<T>.success(items, count, PageNumber, pageSize);
        }
       

    }
}
