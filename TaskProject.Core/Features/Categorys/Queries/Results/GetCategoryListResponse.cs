using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskProject.Core.Features.Categorys.Queries.Results
{
    public class GetCategoryListResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? parentCategorysId { get; set; }
    }
}
