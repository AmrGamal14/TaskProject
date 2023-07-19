using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskProject.Core.Features.Products.Queries.Results;
using TaskProject.Data.Entities;

namespace TaskProject.Core.Mapping.Products
{
    public partial class ProductProfile :Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, GetProductListResponse>();
            AddProductCommand();
            EditProductCommand();
            EditProducGetProductListByCategoryIdMappingtCommand();
        }
        
    }
}
