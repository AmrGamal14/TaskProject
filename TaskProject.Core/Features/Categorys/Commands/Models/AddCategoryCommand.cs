using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskProject.Core.Bases;
using TaskProject.Data.Entities;

namespace TaskProject.Core.Features.Categorys.Commands.Models
{
    public class AddCategoryCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? parentCategorysId { get; set; }
       
    }
}
