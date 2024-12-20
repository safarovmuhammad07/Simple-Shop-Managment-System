using Domein.Dtos;
using Domein.Entities;
using Domein.Responses;
using Infrastructure.Interface;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;
[ApiController]
[Route("[controller]")]
public class OrderController(IOrderService orderService) : ControllerBase
{
    [HttpGet]
    public async Task<Response<List<Order>>> GetOrders()=> await orderService.GetOrders();

    [HttpGet("{id}")]
    public async Task<Response<Order>> GetOrder(int id) => await orderService.GetOrderById(id);
    
    [HttpPost]
    public async Task<Response<bool>> AddOrder(OrderDto order) => await orderService.AddOrder(order);
    
    [HttpPut]
    public async Task<Response<bool>> UpdateOrder(Order order) => await orderService.UpdateOrder(order);
    
    [HttpDelete]
    public async Task<Response<bool>> DeleteOrder(int id) => await orderService.DeleteOrder(id);
    
   
}