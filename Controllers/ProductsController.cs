using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReviewProdutosEcommerce.API.Entities;
using ReviewProdutosEcommerce.API.Models;
using ReviewProdutosEcommerce.API.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewProdutosEcommerce.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ReviewsProdutosDbContect _dbContect;
        private readonly IMapper _mapper;

        public ProductsController(ReviewsProdutosDbContect dbContect, IMapper mapper)
        {
            _dbContect = dbContect;
            _mapper = mapper;
        }

        // GET para api/products
        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _dbContect.Products;

            //sem AutoMapper
            //var productsViewModel = products.Select(p => new ProductViewModel(p.Id, p.Title, p.Price));
           
            //com AutoMapper
            var productsViewModel = _mapper.Map<List<ProductViewModel>>(products);

            return Ok(productsViewModel);
        }

        // GET para api/products/{id}
        [HttpGet("{id}")]
        public IActionResult GetById( int id)
        {
            //se não achar retornar NotFound()
            var products = _dbContect.Products.SingleOrDefault(p => p.Id == id);

            if (products == null)
            {
                return NotFound();
            }

            //sem AutoMapper
            //var reviewsViewModel = products
            //    .Reviews
            //    .Select(r => new ProductReviewViewModel(r.Id, r.Author, r.Rating, r.Comments, r.RegisteredAt))
            //    .ToList();

            //var productDetails = new ProductDetailsViewModel(
            //    products.Id,
            //    products.Title,
            //    products.Description,
            //    products.Price,
            //    products.RegisteredAt,
            //    reviewsViewModel);

            //com AutoMapper
            var productDetails = _mapper.Map<ProductDetailsViewModel>(products);

            return Ok(productDetails);
        }

        // POST para api/products
        [HttpPost]
        public IActionResult Post(AddProductsInputModel model)
        {
            // se tiver erros de validação, retornar BadRequest()
            var product = new Product(model.Title, model.Description, model.Price);

            _dbContect.Products.Add(product);

            return CreatedAtAction(nameof(GetById), new { id = 1 }, model);
        }

        // PUT para api/products/{id}
        [HttpPut("{id}")]
        public IActionResult Put(int id, UpdateProductsInputModel model)
        {
            // se tiver erros de validação, reornar BadRequest()
            // se não existir produto com id especificado, retornar NotFound()

            if(model.Description.Length > 50)
            {
                return BadRequest();
            }

            var product = _dbContect.Products.SingleOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            product.Update(model.Description, model.Price);

            return NoContent();
        }
    }
}
