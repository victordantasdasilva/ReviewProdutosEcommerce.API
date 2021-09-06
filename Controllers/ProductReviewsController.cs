using Microsoft.AspNetCore.Mvc;
using ReviewProdutosEcommerce.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewProdutosEcommerce.API.Controllers
{
    [ApiController]
    [Route("api/products/{productId}/productsreviews")]
    public class ProductReviewsController : ControllerBase
    {
        // GET api/products/1/productreviews/5
        [HttpGet("{id}")]
        public IActionResult GetById(int productId, int id)
        {
            // se não existir com id especificado, retornar NotFound()

            return Ok();
        }

        [HttpPost]
        public IActionResult Post(int productId, AddProductReviewInputModel model)
        {
            // se estiver com dados inválidos, retornar BadRequest()

            return CreatedAtAction(nameof(GetById), new { id = 1, productId = 2 }, model);
        }
    }
}
