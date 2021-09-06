using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewProdutosEcommerce.API.Models
{
    public class AddProductReviewInputModel
    {
        public int Rating { get; set; }
        public string Author { get; set; }
        public string Comments { get; set; }
    }
}
