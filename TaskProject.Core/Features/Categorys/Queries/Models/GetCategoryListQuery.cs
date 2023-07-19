using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskProject.Core.Bases;
using TaskProject.Core.Features.Categorys.Queries.Results;
using TaskProject.Data.Entities;

namespace TaskProject.Core.Features.Categorys.Queries.Models
{
    public class GetCategoryListQuery : IRequest <Response<List<GetCategoryListResponse>>>
    {
    }
}
