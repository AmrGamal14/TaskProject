using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskProject.Data.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double? Price { get; set; }
        public int Availablequantity { get; set; }
        public int Quantitylimit { get; set; }
        public string? ImageUrl { get; set; }
        public string? ImagePath { get; set; }
        public string briefdescription { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category category { get; set; }

    }
}
