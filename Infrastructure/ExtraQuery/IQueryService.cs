using Domein.Dtos;
using Domein.Entities;
using Domein.Responses;

namespace Infrastructure.ExtraQuery;

public interface IQueryService
{
    public Task<Response<List<Order>>> GetOrderByPeriob(DateTime startDate, DateTime endDate);
    public Task<Response<ProductWithTotalPriceOrdersDto>> GetProductWithTotalPriceOrders(int productId);
    public Task<Response<List<ProductOrderDto>>> GetTop3Products();
    public Task<Response<List<OrederCountWithDateDto>>> GetOrederCountInDate();
    public Task<Response<List<Product>>> GetProductsWithStock(int stock);
    public Task<Response<Product>> GetExpenisivProduct();
}