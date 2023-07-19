using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskProject.Infrastructure.InfrastructureBases
{
    public interface IGenericeRepositoryAsync <T> where T : class
    {
        Task DeleteRangeAsync(ICollection<T> entities);
        Task <T> GetByIdAsync(int id);
        Task SaveChangesAsync();
        IDbContextTransaction BeginTransaction();
        void commit();
        void RollBack();
        IQueryable <T> GetTableNoTracking();
        IQueryable <T> GetTableAsTracking();
        Task <T> AddAsync(T entity);
        Task AddRangeAsync(ICollection<T> entity);
        Task UpdateAsync(T entity);
        Task UpdateRangeAsync(ICollection<T> entity);
        Task DeleteAsync(T entity);

    }
}
