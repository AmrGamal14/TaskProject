using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskProject.Core.Features.Products.Commands.Models;
using TaskProject.Data.Entities;

namespace TaskProject.Core.Mapping.Products
{
    public partial class ProductProfile
    {
        public void EditProductCommand()
        {

            CreateMap<EditProductCommand, Product>();
        }
    }
}
