using Catalog.API.Entities;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Controller
{
     [ApiController]
     [Route("api/v1/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository _repository;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(IProductRepository repository, ILogger<CatalogController> logger)
        {
            _repository = repository;
            _logger = logger;
            //_repository = repository?? new ArgumentNullException(nameof(repository));
            //_logger = logger ?? new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>),(int)StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProucts()
        {
            var products = await _repository.GetProducts();
            if (products == null)
                return NotFound ();
            return Ok(products);
        }
        //[HttpGet("{id:length(24)}",Name = "GetProduct")]
        [HttpGet]
        [Route("[action]/{id}", Name = "GetProductById")]
        [ProducesResponseType(typeof(Product), (int)StatusCodes.Status200OK)]
        [ProducesResponseType((int)StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Product>> GetProductById(string id)
        {
            var product = await _repository.GetProductById(id);
            return Ok(product);
        }

        [HttpGet]
        [Route("[action]/{category}",Name = "GetProductByCategory")]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProuctsByCategory(string category)
        {
            var products = await _repository.GetProductByCategory(category);
            if (products == null)
                return NotFound();
            return Ok(products);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Product),(int)StatusCodes.Status200OK)]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            await _repository.CreateProduct(product);
            return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        }
        [HttpPut]
        [ProducesResponseType(typeof(Product), (int)StatusCodes.Status200OK)]
        public async Task<ActionResult<Product>> UpdateProduct([FromBody] Product product)
        {
            await _repository.UpdateProduct(product);
            return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        }

        [HttpDelete("{id:length(24)}", Name = "DeleteProduct")]
        [ProducesResponseType(typeof(Product), (int)StatusCodes.Status200OK)]
        public async Task<ActionResult<Product>> DeleteProduct([FromBody] Product product)
        {
            await _repository.DeleteProduct(product);
            return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        }
    }
}
