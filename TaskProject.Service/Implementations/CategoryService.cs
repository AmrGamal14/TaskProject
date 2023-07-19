using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskProject.Data.Entities;
using TaskProject.Infrastructure.Abstracts;
using TaskProject.Infrastructure.Repositories;
using TaskProject.Service.Abstracts;

namespace TaskProject.Service.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository=categoryRepository;
        }
        public async Task<List<Category>> GetCategorysListAsync()
        {
            return await _categoryRepository.GetCategorysListAscync();
        }
        public async Task<string> AddAsync(Category category)
        {
            var categoryResult = await _categoryRepository.GetTableNoTracking().Where(x => x.Name.Equals(category.Name)).FirstOrDefaultAsync();
            if (categoryResult != null) return "Exist";
            await _categoryRepository.AddAsync(category);
            return "Success";
        }
        public async Task<Category> GetProductByIdasync(int id)
        {
            var category = _categoryRepository.GetTableNoTracking().Where(x => x.Id == id).FirstOrDefault();
            return category;
        }
        public async Task<string> DeleteAsync(Category category)
        {
            await _categoryRepository.DeleteAsync(category);
            return "Success";
        }

        
    }
}
