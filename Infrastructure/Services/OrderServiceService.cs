using System.Net;
using Dapper;
using Domein.Dtos;
using Domein.Entities;
using Domein.Responses;
using Infrastructure.DataContext;
using Infrastructure.Interface;

namespace Infrastructure.Services;

public class OrderServiceService(IContext _context) : IOrderService
{
    public async Task<Response<List<Order>>> GetOrders()
    {
        try
        {
            var sql = @"select * from Orders";
            var res = await _context.Connection().QueryAsync<Order>(sql);
            return new Response<List<Order>>(res.ToList());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Response<Order>> GetOrderById(int id)
    {
        try
        {
            const string sql = @"select * from Orders where OrderId = @id";
            var res = await _context.Connection().QuerySingleOrDefaultAsync<Order>(sql, new { id });
            return new Response<Order>(res);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Response<bool>> AddOrder(OrderDto order)
    {
        try
        {
            const string sql = @"insert into Orders (ProductId, Quantity, TotalPrice, OrderDate) ";
            var res = await _context.Connection().ExecuteAsync(sql, order);
            return res == 0 
                ? new Response<bool>(HttpStatusCode.InternalServerError, "Internal Server Error")
                : new Response<bool>(HttpStatusCode.Created, "Order added");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Response<bool>> UpdateOrder(Order order)
    {
        try
        {
            const string sql = @"update Orders set ProductId=@ProductId, Quantity=@Quantity, TotalPrice=@TotalPrice, OrderDate=@OrderDate where OrderId = @OrderId";
            var res = await _context.Connection().ExecuteAsync(sql, order);
            return res == 0 
                ? new Response<bool>(HttpStatusCode.InternalServerError, "Internal Server Error")
                : new Response<bool>(HttpStatusCode.Created, "Order updated");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Response<bool>> DeleteOrder(int id)
    {
        try
        {
            const string sql = @"delete from Orders where OrderId = @id";
            var res = await _context.Connection().ExecuteAsync(sql, new { id });
            return res == 0 
                ? new Response<bool>(HttpStatusCode.InternalServerError, "Internal Server Error")
                : new Response<bool>(HttpStatusCode.Created, "Order deleted");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    
}