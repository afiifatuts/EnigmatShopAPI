using EnigmaShopApi.Entities;
using EnigmaShopApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace EnigmaShopApi.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateNewProduct([FromBody] Product payload)
    {
        var product = await _productService.Create(payload);
        return Created("/api/products", product);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProduct()
    {
        //throw new Exception();
        var products = await _productService.GetAll();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetProductById(string id)
    {
        var product = await _productService.GetById(id);
        return Ok(product);
    }
}