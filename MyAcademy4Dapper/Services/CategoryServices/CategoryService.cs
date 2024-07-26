using Dapper;
using MyAcademy4Dapper.Context;
using MyAcademy4Dapper.Dtos.CategoryDtos;

namespace MyAcademy4Dapper.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly DapperContext _context;

        public CategoryService(DapperContext context)
        {
            _context = context;
        }

        public async Task CreateCategoryAsync(CreateCategoryDto categoryDto)
        {
            string query = "insert into categories (CategoryName) values (@categoryName)";
            var parameters = new DynamicParameters();
            parameters.Add("@categoryName", categoryDto.CategoryName);
            var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query,parameters);
 
        }

        public async Task DeleteCategoryAsync(int id)
        {
            string query = "delete from categories where categoryId=@CategoryId";
            var parameters = new DynamicParameters();
            parameters.Add("@CategoryId", id);
            var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, parameters);
        }

        public async Task<List<ResultCategoryDto>> GetAllCategoriesAsync()
        {
            string query = "select * from categories";
            var connection = _context.CreateConnection();
           var values =  await connection.QueryAsync<ResultCategoryDto>(query);
            return values.ToList();
        }

        public async Task<ResultCategoryByIdDto> GetCategoryByIdAsync(int id)
        {
            string query = "select * from categories where categoryId=@CategoryId";
            var parameters = new DynamicParameters();
            parameters.Add("@CategoryId", id);
            var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<ResultCategoryByIdDto>(query,parameters);
          
        }

        public async Task UpdateCategoryAsync(UpdateCategoryDto categoryDto)
        {
            string query = "update categories set CategoryName=@CategoryName where CategoryId=@CategoryId";
            var parameters = new DynamicParameters(categoryDto);
            var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, parameters);
        }
    }
}
