using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskProject.Data.Entities;
using TaskProject.Infrastructure.InfrastructureBases;

namespace TaskProject.Infrastructure.Abstracts
{
    public interface IProductRepository : IGenericeRepositoryAsync<Product>
    {
        public Task<List<Product>> GetProductListAscync();  
        public Task<List<Product>> GetProductListByCategoryIdAscync(int id, int pagenumber, int pagesize);
        public Task<List<Product>> GetProductListByCategoryId(int id);
    }
}
