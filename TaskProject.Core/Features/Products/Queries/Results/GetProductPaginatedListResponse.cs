using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskProject.Core.Features.Products.Queries.Results
{
    public class GetProductPaginatedListResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double? Price { get; set; }
        public int? Availablequantity { get; set; }
        public int? Quantitylimit { get; set; }
        public string ImageUrl { get; set; }
        public string? briefdescription { get; set; }
        public GetProductPaginatedListResponse(int id, string name, double? price, int? availablequantity, int? quantitylimit, string imageUrl, string? Briefdescription)
        {
            Id=id;
            Name=name;
            Price=price;
            Availablequantity=availablequantity;
            Quantitylimit=quantitylimit;
            ImageUrl=imageUrl;
            briefdescription=Briefdescription;
        }
    }
}
