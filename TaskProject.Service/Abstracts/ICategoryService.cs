using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskProject.Data.Entities;

namespace TaskProject.Service.Abstracts
{
    public interface ICategoryService
    {
        public Task<List<Category>> GetCategorysListAsync();
        public Task<string> AddAsync(Category category);
        public Task<Category> GetProductByIdasync(int id);
        public Task<string> DeleteAsync(Category category);
    }
}
