using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskProject.Core.Features.Categorys.Commands.Models;
using TaskProject.Data.Entities;

namespace TaskProject.Core.Mapping.Categorys
{
    public partial  class CategoryProfile
    {
        public void AddCategoryCommand()
        {


                CreateMap<AddCategoryCommand,Category>();


        }
    }
}
