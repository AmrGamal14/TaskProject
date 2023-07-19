using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskProject.Data.Entities;
using TaskProject.Infrastructure.Abstracts;
using TaskProject.Service.Abstracts;

namespace TaskProject.Service.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService (IProductRepository productRepository)
        {
            _productRepository=productRepository;
        }

       

        public async Task<List<Product>> GetProductsListAsync()
        {
          return await  _productRepository.GetProductListAscync();
        }
        public async Task<string> AddAsync(Product product)
        {
            var productResult =await _productRepository.GetTableNoTracking().Where(x => x.Name.Equals(product.Name)).FirstOrDefaultAsync();
            if (productResult != null) return "Exist";
           await _productRepository.AddAsync(product);
            return "Success";
        }

      public async Task<Product> GetProductByIdasync(int id)
        {
            var product = _productRepository.GetTableNoTracking().Where(x => x.Id == id).FirstOrDefault();
            return product;
        }

        public async Task<string> EditAsync(Product product)
        {
          //  var ss = _productRepository.GetTableNoTracking().Where(x => x.Id ==id
            await _productRepository.UpdateAsync(product);
            return "Success";
        }

        public async  Task<string> DeleteAsync(Product product)
        {

            await _productRepository.DeleteAsync(product);
            return "Success";
        }

        public IQueryable<Product> GetProductQuerable()
        {
            return _productRepository.GetTableNoTracking().AsQueryable();
        }

        public async Task <List<Product>> GetProductByCategoryIdasync(int Id, int pagenumber, int pagesize)
        {
           
            var product= await _productRepository.GetProductListByCategoryIdAscync(Id, pagenumber, pagesize);
            return product;
            

        }
        

        public async Task<List<Product>> GetProductListByCategoryId(int id)
        {

            var product = await _productRepository.GetProductListByCategoryId(id);
            return product;

        }
    }
}
