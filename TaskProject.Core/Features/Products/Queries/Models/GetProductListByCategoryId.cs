using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskProject.Core.Bases;
using TaskProject.Core.Features.Products.Queries.Results;

namespace TaskProject.Core.Features.Products.Queries.Models
{
    public class GetProductListByCategoryId : IRequest<Response<List<GetProductListByCategoryIDResponse>>>
    {
       public  int Id { get; set; }
        public int pagenumber { get; set; }
        public int pagesize { get; set; }
    }
}
