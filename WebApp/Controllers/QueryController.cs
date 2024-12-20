using Domein.Dtos;
using Domein.Entities;
using Domein.Responses;
using Infrastructure.ExtraQuery;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;
[ApiController]
[Route("[controller]")]
public class QueryController(IQueryService queryService):ControllerBase
{
    [HttpGet("period")]
    public Task<Response<List<Order>>> GetOrderByPeriob(DateTime startDate, DateTime endDate)
    {
        return queryService.GetOrderByPeriob(startDate, endDate);
    }

    [HttpGet("totalprice")]
    public Task<Response<ProductWithTotalPriceOrdersDto>> GetProductWithTotalPriceOrders(int productId)
    {
        return queryService.GetProductWithTotalPriceOrders(productId);
    }

    [HttpGet("top3")]
    public Task<Response<List<ProductOrderDto>>> GetTop3Products()
    {
        return queryService.GetTop3Products();
    }
    
    [HttpGet("inDate")]
    public Task<Response<List<OrederCountWithDateDto>>> GetOrederCountInDate()
    {
        return queryService.GetOrederCountInDate();
        
    }

    [HttpGet("Stock")]
    public Task<Response<List<Product>>> GetProductsWithStock(int stock)
    {
        return queryService.GetProductsWithStock(stock);
    }

    [HttpGet("Expensives")]
    public Task<Response<Product>> GetExpenisivProduct()
    {
        return queryService.GetExpenisivProduct();
    }



}