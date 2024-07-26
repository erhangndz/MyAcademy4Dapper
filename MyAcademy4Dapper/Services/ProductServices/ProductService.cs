using Dapper;
using MyAcademy4Dapper.Context;
using MyAcademy4Dapper.Dtos.ProductDtos;

namespace MyAcademy4Dapper.Services.ProductServices
{
    public class ProductService : IProductService
    {

        private readonly DapperContext _context;

        public ProductService(DapperContext context)
        {
            _context = context;
        }

        public async Task CreateProductAsync(CreateProductDto productDto)
        {
            string query = "insert into products (productName,description,price,ImageUrl,CategoryId) values(@ProductName,@Description,@Price,@ImageUrl,@CategoryId)";
            var parameters = new DynamicParameters(productDto);
            var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, parameters);
        }

        public async Task DeleteProductAsync(int id)
        {
            string query = "delete from products where ProductId=@ProductId";
            var parameters = new DynamicParameters();
            parameters.Add("@ProductId", id);
            var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query,parameters);
        }

        public async Task<List<ResultProductDto>> GetAllProductsAsync()
        {
            string query = "select productId,productName,description,price,ImageUrl,CategoryName from Products inner join Categories on Categories.CategoryId=Products.CategoryId";
            var connection = _context.CreateConnection();
            var values = await connection.QueryAsync<ResultProductDto>(query);
            return values.ToList();

        }

        public async Task<ResultProductByIdDto> GetProductByIdAsync(int id)
        {
            string query = "select * from products where ProductId=@ProductId";
            var parameters = new DynamicParameters();
            parameters.Add("@ProductId", id);
            var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<ResultProductByIdDto>(query, parameters);
        }

        public async Task UpdateProductAsync(UpdateProductDto productDto)
        {
            string query = $"update products set productName=@ProductName,description=@Description,price=@Price,ImageUrl=@ImageUrl,CategoryId=@CategoryId where ProductId=@ProductId";
            var parameters = new DynamicParameters(productDto);
            var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, parameters);
        }
    }
}
