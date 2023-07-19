using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskProject.Data.Entities;
using TaskProject.Infrastructure.InfrastructureBases;

namespace TaskProject.Infrastructure.Abstracts
{
    public interface ICategoryRepository : IGenericeRepositoryAsync<Category>
    {
        public Task<List<Category>> GetCategorysListAscync();

    }
}
