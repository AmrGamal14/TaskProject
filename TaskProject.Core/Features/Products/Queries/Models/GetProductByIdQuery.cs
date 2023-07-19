using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskProject.Core.Bases;
using TaskProject.Core.Features.Products.Queries.Results;
using TaskProject.Data.Entities;

namespace TaskProject.Core.Features.Products.Queries.Models
{
    public class GetProductByIdQuery : IRequest<Response<GetProductListResponse>>
    {
        public int productId { get; set; }
        public GetProductByIdQuery(int productId)
        {
            this.productId=productId;
        }   
    }
}
