using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskProject.Data.Entities;
using TaskProject.Infrastructure.Abstracts;
using TaskProject.Infrastructure.Data;
using TaskProject.Infrastructure.InfrastructureBases;

namespace TaskProject.Infrastructure.Repositories
{

    
        public class CategoryRepository : GenericRepositoryAsync<Category>,ICategoryRepository
        {
            #region Fields
            private readonly DbSet<Category> _Category;
            #endregion
            #region Constructors
            public CategoryRepository(ApplicationDBContext dBContext) : base(dBContext)
        {
            _Category=dBContext.Set<Category>();
            }
            #endregion
            #region Handles Functions 
            public async Task<List<Category>> GetCategorysListAscync()
            {
                return await _Category.ToListAsync();
            }
            #endregion
        }
    }

