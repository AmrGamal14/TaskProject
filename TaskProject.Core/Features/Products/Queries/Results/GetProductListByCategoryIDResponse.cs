using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskProject.Core.Features.Products.Queries.Results
{
    public class GetProductListByCategoryIDResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double? Price { get; set; }
        public int? Availablequantity { get; set; }
        public int? Quantitylimit { get; set; }
        public string ImageUrl { get; set; }
        public string? briefdescription { get; set; }
        
    }
}
