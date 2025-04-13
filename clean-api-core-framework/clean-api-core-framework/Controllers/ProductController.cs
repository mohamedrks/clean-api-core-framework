using Application.DTOs;
using Application.Interfaces;
using clean_api_core_framework.Controllers;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APICoreFramework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<WeatherForecastController> _logger;

        public ProductController(
            ILogger<WeatherForecastController> logger,
            IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProductCreateDto dto, CancellationToken cancellationToken)
        {
            if (dto.Price < 0)
                return BadRequest(ProblemDetailsFactory.CreateProblemDetails(
                    HttpContext, statusCode: 400, detail: "Invalid Price."));
            int productId = await _productService.CreateProductAsync(dto, cancellationToken);

            return Ok(new { Id = productId });
        }

    }
}