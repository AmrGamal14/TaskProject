using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskProject.Core.Features.Categorys.Queries.Models;
using TaskProject.Core.Features.Categorys.Queries.Results;
using TaskProject.Core.Features.Products.Queries.Results;
using TaskProject.Data.Entities;

namespace TaskProject.Core.Mapping.Categorys
{
    public partial class CategoryProfile :Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, GetCategoryListResponse>();

            AddCategoryCommand();

        }
        
    }
}
