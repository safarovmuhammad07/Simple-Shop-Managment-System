using Domein.Dtos;
using Domein.Entities;
using Domein.Responses;

namespace Infrastructure.Interface;

public interface IProductService
{
    public Task<Response<List<Product>>> GetProducts();
    public Task<Response<Product>> GetProductById(int id);
    public Task<Response<bool>> AddProduct(ProductDto product);
    public Task<Response<bool>> UpdateProduct(Product product);
    public Task<Response<bool>> DeleteProduct(int id);
}