using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewProdutosEcommerce.API.Models
{
    public class UpdateProductsInputModel
    {
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
