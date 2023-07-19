
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskProject.Core.Bases;

namespace TaskProject.Core.Features.Products.Commands.Models
{
    public class AddProductCommand: IRequest<Response<string>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Availablequantity { get; set; }
        public int Quantitylimit { get; set; }
        public IFormFile Image { get; set; }

        public string briefdescription { get; set; }
        public int CategoryId { get; set; }
    }
}
