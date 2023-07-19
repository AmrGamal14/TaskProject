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
    public class GetProductListQuery : IRequest<Response<List<GetProductListResponse>>>
    {
    }
}
