using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReviewProdutosEcommerce.API.Entities;
using ReviewProdutosEcommerce.API.Models;
using ReviewProdutosEcommerce.API.Persistence.Repositories;
using Serilog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReviewProdutosEcommerce.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET para api/products
        /// <summary>
        /// Consulta de uma lista de produtos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _repository.GetAllAsync();

            var productsViewModel = _mapper.Map<List<ProductViewModel>>(products);

            return Ok(productsViewModel);
        }

        // GET para api/products/{id}
        /// <summary>
        /// Consulta de um produto
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById( int id)
        {
            var product = await _repository.GetDetailsByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            var productDetails = _mapper.Map<ProductDetailsViewModel>(product);

            return Ok(productDetails);
        }

        // POST para api/products
        /// <summary>
        /// Cadastro de Produto
        /// </summary>
        /// <remarks>
        /// Requisição:<br/>
        /// {<br/>
        ///    "title": "Notebook",<br/>
        ///    "description": "Notebook Dell",<br/>
        ///    "price": 3000<br/>
        /// }<br/>
        /// </remarks>
        /// 
        /// <param name="model">Objeto com dados de cadstro de Produto</param>
        /// <returns>Objeto criado</returns>
        /// <response code="201">Sucesso</response>
        /// <response code="400">Dados inválidos</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(AddProductInputModel model)
        {
            var product = new Product(model.Title, model.Description, model.Price);

            Log.Information("Método POST chamado!");

            await _repository.AddAsync(product);

            return CreatedAtAction(nameof(GetById), new { id = 1 }, model);
        }

        // PUT para api/products/{id}
        /// <summary>
        /// Atualização de um produto
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateProductsInputModel model)
        {
            var product = await _repository.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            product.Update(model.Description, model.Price);
            await _repository.UpdateAsync(product);

            return NoContent();
        }
    }
}
