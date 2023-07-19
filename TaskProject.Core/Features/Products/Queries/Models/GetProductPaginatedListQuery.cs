using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskProject.Core.Features.Products.Queries.Results;
using TaskProject.Core.Wrappers;

namespace TaskProject.Core.Features.Products.Queries.Models
{
    public class GetProductPaginatedListQuery: IRequest<PaginatedResult<GetProductPaginatedListResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string[]? OrderBy { get; set; }
        public string? Search { get; set; }
    }
}
