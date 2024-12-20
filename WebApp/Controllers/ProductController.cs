using Domein.Dtos;
using Domein.Entities;
using Domein.Responses;
using Infrastructure.Interface;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController(IProductService productService) : ControllerBase
{
    [HttpGet]
    public async Task<Response<List<Product>>> GetProducts() => await productService.GetProducts();

    [HttpGet("{id}")]
    public async Task<Response<Product>> GetProduct(int id) => await productService.GetProductById(id);
    
    [HttpPost]
    public async Task<Response<bool>> AddProduct(ProductDto product)=> await productService.AddProduct(product);
    
    [HttpPut]
    public async Task<Response<bool>> UpdateProduct(Product product)=> await productService.UpdateProduct(product);
    
    [HttpDelete]
    public async Task<Response<bool>> DeleteProduct(int id)=> await productService.DeleteProduct(id);
    
   
}