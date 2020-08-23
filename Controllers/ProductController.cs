using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace APIExample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<ProductViewModel> products = _productRepository.FindAll();
                if (products.Any())
                    return Ok(products);
                return NoContent();
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpPost]
        public IActionResult Post(ProductViewModel product)
        {
            try
            {
                ProductViewModel result = _productRepository.Create(product);
                if (result != null)
                    return Ok(result);
                        return StatusCode(500);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }
    }
}
