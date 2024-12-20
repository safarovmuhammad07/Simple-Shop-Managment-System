using Domein.Dtos;
using Domein.Entities;
using Domein.Responses;

namespace Infrastructure.Interface;

public interface IOrderService
{
    public Task<Response<List<Order>>> GetOrders();
    public Task<Response<Order>> GetOrderById(int id);
    public Task<Response<bool>> AddOrder(OrderDto order);
    public Task<Response<bool>> UpdateOrder(Order order);
    public Task<Response<bool>> DeleteOrder(int id);

}