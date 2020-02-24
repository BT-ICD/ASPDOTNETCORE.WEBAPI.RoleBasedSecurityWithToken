using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore3JWT.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public static List<Product> GetProducts()
        {
            return new List<Product>()
            {
                new Product{Id=101,Name="Dettol"},
                new Product{Id=102,Name="Cinthol"}

            };
        }
    }
}
