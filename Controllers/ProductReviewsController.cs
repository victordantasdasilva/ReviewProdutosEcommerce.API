using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReviewProdutosEcommerce.API.Entities;
using ReviewProdutosEcommerce.API.Models;
using ReviewProdutosEcommerce.API.Persistence.Repositories;
using System.Threading.Tasks;

namespace ReviewProdutosEcommerce.API.Controllers
{
    [ApiController]
    [Route("api/products/{productId}/productsreviews")]
    public class ProductReviewsController : ControllerBase
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductReviewsController(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET api/products/1/productreviews/5
        /// <summary>
        /// Consulta de uma avaliação de um produto
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int productId, int id)
        {
            var productReview = await _repository.GetReviewByIdAsync(id);

            if (productReview == null)
            {
                return NotFound();
            }
            var productDetails = _mapper.Map<ProductReviewDetailsViewModel>(productReview);

            return Ok(productDetails);
        }
        /// <summary>
        /// Cadastro de uma avaliação de um produto
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(int productId, AddProductReviewInputModel model)
        {
            var productReview = new ProductReview(model.Author, model.Rating, model.Comments, productId);

            await _repository.AddReviewAsync(productReview);

            return CreatedAtAction(nameof(GetById), new { id = productReview.Id, productId = productId }, model);
        }
    }
}
