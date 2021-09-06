using ReviewProdutosEcommerce.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewProdutosEcommerce.API.Persistence
{
    public class ReviewsProdutosDbContect
    {
        public ReviewsProdutosDbContect()
        {
            Products = new List<Product>();
        }
        public List<Product> Products { get; set; }
    }
}
