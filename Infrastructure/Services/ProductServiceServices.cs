using System.Net;
using Dapper;
using Domein.Dtos;
using Domein.Entities;
using Domein.Responses;
using Infrastructure.DataContext;
using Infrastructure.Interface;

namespace Infrastructure.Services;

public class ProductServiceServices(IContext _context) : IProductService
{
    public async Task<Response<List<Product>>> GetProducts()
    {
        try
        {
            const string sql = @"select * from Products";
            var res = await _context.Connection().QueryAsync<Product>(sql);
            return new Response<List<Product>>(res.ToList());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Response<Product>> GetProductById(int id)
    {
        try
        {
            const string sql = @"select * from Products where ProductId = @id";
            var res = await _context.Connection().QuerySingleOrDefaultAsync<Product>(sql, new { id });
            return new Response<Product>(res);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Response<bool>> AddProduct(ProductDto product)
    {
        try
        {
            const string sql = @"insert into Products (ProductName,Price,Stock ) values (@ProductName, @Price, @Stock)";
            var res = await _context.Connection().ExecuteAsync(sql, product);
            return res == 0 
                ? new Response<bool>(HttpStatusCode.InternalServerError, "Internal Server Error")
                : new Response<bool>(HttpStatusCode.Created, "Product added successfully");

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Response<bool>> UpdateProduct(Product product)
    {
        try
        {
            const string sql = @"update Products set ProductName = @ProductName, Price=@Price, Stock=@Stock where ProductId = @ProductId";
            var res = await _context.Connection().ExecuteAsync(sql, product);
            return res == 0 
                ? new Response<bool>(HttpStatusCode.InternalServerError, "Internal Server Error")
                : new Response<bool>(HttpStatusCode.OK, "Product updated successfully");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Response<bool>> DeleteProduct(int id)
    {
        try
        {
            const string sql = @"delete from Products where ProductId = @id";
            var res = await _context.Connection().ExecuteAsync(sql, new { id });
            return res == 0 
                ? new Response<bool>(HttpStatusCode.InternalServerError, "Internal Server Error")
                : new Response<bool>(HttpStatusCode.OK, "Product deleted successfully");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    

}