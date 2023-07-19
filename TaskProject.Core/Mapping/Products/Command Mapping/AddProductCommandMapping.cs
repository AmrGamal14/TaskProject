using Azure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskProject.Core.Features.Products.Commands.Models;
using TaskProject.Core.Features.Products.Queries.Results;
using TaskProject.Data.Entities;

namespace TaskProject.Core.Mapping.Products
{
    public partial class ProductProfile
    {
        public void AddProductCommand()
        {

            CreateMap<AddProductCommand, Product>();
                


        }
    }
}
