using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskProject.Data.Entities;

namespace TaskProject.Service.Abstracts
{
    public interface IProductService
    {
        public Task<List<Product>> GetProductsListAsync();
        public Task<string> AddAsync(Product product);
        public Task <Product > GetProductByIdasync (int id);  
        public Task<List<Product>> GetProductByCategoryIdasync (int Id, int pagenumber, int pagesize);
        public Task<List<Product>> GetProductListByCategoryId(int id);
        public Task<string> EditAsync(Product product);
        public Task<string> DeleteAsync(Product product);
        public IQueryable<Product> GetProductQuerable();

        
    }
}
 