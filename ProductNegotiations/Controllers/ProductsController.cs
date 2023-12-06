using FluentValidation;
using FluentValidation.Results;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProductNegotiations.API.Models;
using ProductNegotiations.API.Validators;
using ProductNegotiations.Database.Library.Models;
using ProductNegotiations.Library.Helpers;
using ProductNegotiations.Library.Models;
using ProductNegotiations.Library.Services;

namespace ProductNegotiations.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IProductService _service;
        private readonly IValidator<ProductClientModel> _validator;

        public ProductsController(ILogger<ProductsController> logger, IProductService service, IValidator<ProductClientModel> validator)
        {
            _logger = logger;
            _service = service;
            _validator = validator;
        }
        [HttpGet("api/Products/{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            try
            {
                var data = await _service.GetProductByIdAsync(id);
                return Ok(data.Adapt<ProductClientModel>());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest();
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery] PagingModel paging)
        {
            try
            {
                var data = await _service.GetAllProductsAsync(paging);
                var metadata = new
                {
                    data.TotalCount,
                    data.PageSize,
                    data.CurrentPage,
                    data.TotalPages,
                    data.HasNext,
                    data.HasPrevious
                };
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
                return Ok(data.Adapt<IEnumerable<ProductClientModel>>());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest();
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateProduct(ProductClientModel product)
        {
            try
            {
                ValidationResult result = await _validator.ValidateAsync(product);
                if (!result.IsValid)
                {
                    return BadRequest("Validation error");
                }

                var data = await _service.CreateProductAsync(product.Adapt<ProductModel>());
                return Created(HttpContext.Request.Host.ToString() + "/" + data?.Id, data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest();
            }
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProduct(ProductClientModel product)
        {
            try
            {
                await _service.UpdateProduct(product.Adapt<ProductModel>());
                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest();
            }
        }
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct(ProductClientModel product)
        {
            try
            {
                await _service.DeleteProduct(product.Adapt<ProductModel>());
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest();
            }
        }
    }
}
