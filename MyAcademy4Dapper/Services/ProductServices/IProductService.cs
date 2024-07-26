using MyAcademy4Dapper.Dtos.ProductDtos;

namespace MyAcademy4Dapper.Services.ProductServices
{
    public interface IProductService
    {
        Task<List<ResultProductDto>> GetAllProductsAsync();

        Task<ResultProductByIdDto> GetProductByIdAsync(int id);

        Task DeleteProductAsync(int id);

        Task UpdateProductAsync(UpdateProductDto productDto);

        Task CreateProductAsync(CreateProductDto productDto);

    }
}
