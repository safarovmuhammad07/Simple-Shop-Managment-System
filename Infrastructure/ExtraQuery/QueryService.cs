using System.Net;
using Dapper;
using Domein.Dtos;
using Domein.Entities;
using Domein.Responses;
using Infrastructure.DataContext;
using Infrastructure.Interface;

namespace Infrastructure.ExtraQuery;

public class QueryService(IContext _context) : IQueryService
{
    
    public async Task<Response<List<Order>>> GetOrderByPeriob(DateTime startDate, DateTime endDate)
    {
        try
        {
            const string sql = @"select * from Orders where OrderDate >= @startDate and OrderDate <= @endDate";
            var res = await _context.Connection().QueryAsync<Order>(sql, new { startDate, endDate });
            return new Response<List<Order>>(res.ToList());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Response<ProductWithTotalPriceOrdersDto>> GetProductWithTotalPriceOrders(int productId)
    {
        try
        {
            const string sql = @"select * from Products where ProductId = @productId";
            var res = await _context.Connection().QuerySingleOrDefaultAsync<ProductWithTotalPriceOrdersDto>(sql, new { productId });
            
            const string sqlOrders = @"select * from Orders where ProductId = @productId";
            var orders = await _context.Connection().QueryAsync<Order>(sqlOrders, new { productId });
            
            const string sqltotalPriceOrders = @"select sum(TotalPrice) from Orders where ProductId = @productId";
            var totalPriceOrders = await _context.Connection().QuerySingleOrDefaultAsync<decimal>(sqltotalPriceOrders, new { productId });
            
            res.TotalPriceOrders = totalPriceOrders;
            
            res.Orders = orders.ToList();

            return new Response<ProductWithTotalPriceOrdersDto>(res);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Response<List<ProductOrderDto>>> GetTop3Products()
    {
        try
        {
            const string sql = @"select p.ProductId as ProductId , p.ProductName as ProductName, p.Price as Price, p.Stock as Stock, count(o.orderId) as OrderCount  from Products as p join Orders as o on p.ProductId = o.OrderId group by p.ProductId , p.ProductName, p.Price, p.Stock order by count(o.orderId) desc  limit 3";
            var res = await _context.Connection().QueryAsync<ProductOrderDto>(sql);
            return new Response<List<ProductOrderDto>>(res.ToList());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Response<List<OrederCountWithDateDto>>> GetOrederCountInDate()
    {
        try
        {
            const string sql = @"SELECT DATE(OrderDate) AS DateOrder, COUNT(*) AS CountOrder  FROM Orders GROUP BY DATE(OrderDate) ORDER BY  DateOrder";
            var res = await _context.Connection().QueryAsync<OrederCountWithDateDto>(sql);
            return new Response<List<OrederCountWithDateDto>>(res.ToList());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Response<List<Product>>> GetProductsWithStock(int stock)
    {
        try
        {
            const string sql = @"select * from Products where Stock > @stock";
            var res = await _context.Connection().QueryAsync<Product>(sql, new { stock });
            return new Response<List<Product>>(res.ToList());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Response<Product>> GetExpenisivProduct()
    {
        try
        {
            const string sql = @"select * from Products  order by Price desc  limit 1";
            var res = await _context.Connection().QuerySingleOrDefaultAsync<Product>(sql);
            return res != null ? new Response<Product>(res) : new Response<Product>(HttpStatusCode.NotFound,"Not found");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}