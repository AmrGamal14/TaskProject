using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskProject.Data.Entities
{
    public class Category
    {

        public int Id { get; set; }

        public string Name { get; set; }
        public List<Product> products { get; set; }
        
        public int? parentCategorysId { get; set; }
        [ForeignKey("parentCategorysId")]
        public List <Category> categorys { get; set; }




    }
}
