using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskProject.Core.Bases;

namespace TaskProject.Core.Features.Categorys.Commands.Models
{
    public class DeleteCategoryCommand : IRequest<Response<string>>
    {
       
            public int Id { get; set; }
            public DeleteCategoryCommand(int id)
            {
                Id = id;

            }
       
    }
}
