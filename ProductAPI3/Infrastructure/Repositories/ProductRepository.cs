using Dapper;
using Microsoft.Data.SqlClient;
using ProductAPI3.Application.Interfaces;
using ProductAPI3.Domain.Entities;
using System.Data;

namespace ProductAPI3.Infrastructure.Repositories
{
    public class ProductRepository(IConfiguration config) : IProductRepository
    {
        private readonly string _connectionString = config.GetConnectionString("DefaultConnection")!;

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            var sql = "SELECT * FROM Test_Product";
            return await db.QueryAsync<Product>(sql);
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            var sql = "SELECT * FROM Test_Product WHERE Id = @Id";
            return await db.QueryFirstOrDefaultAsync<Product>(sql, new { Id = id });
        }

        public async Task<Product> CreateAsync(Product product)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            var sql = @"
            INSERT INTO Test_Product (Name, Description, Price, Stock)
            OUTPUT INSERTED.*
            VALUES (@Name, @Description, @Price, @Stock)";
            return await db.QuerySingleAsync<Product>(sql, product);
        }

        public async Task UpdateAsync(Product product)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            var sql = @"
            UPDATE Test_Product 
            SET Name = @Name, Description = @Description, Price = @Price, Stock = @Stock
            WHERE Id = @Id";
            await db.ExecuteAsync(sql, product);
        }

        public async Task DeleteAsync(int id)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            var sql = "DELETE FROM Test_Product WHERE Id = @Id";
            await db.ExecuteAsync(sql, new { Id = id });
        }
    }

}