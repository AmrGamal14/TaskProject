using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskProject.Data.Entities;
using TaskProject.Infrastructure.Abstracts;
using TaskProject.Infrastructure.Data;
using TaskProject.Infrastructure.InfrastructureBases;

namespace TaskProject.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepositoryAsync<Product>,IProductRepository
    {
        #region Fields
        private readonly DbSet<Product> _products;
        #endregion
        #region Constructors
        public ProductRepository(ApplicationDBContext dBContext):base(dBContext)
        {
            _products=dBContext.Set<Product>();
        }
        #endregion
        #region Handles Functions 
        public async Task<List<Product>> GetProductListAscync()
        {
            
            return await _products.ToListAsync();
        }

        public async  Task<List<Product>> GetProductListByCategoryIdAscync(int id, int pagenumber, int pagesize)
        {
           
            return  _products.FromSql($"exec SearchByCategoryID {id},{pagenumber},{pagesize} ").ToList();
            
            


        }
        public async Task<List<Product>> GetProductListByCategoryId(int id)
        {

            var products = await _products.Where(x => x.CategoryId == id).ToListAsync();

            return products;
        }
        #endregion
    }
}
