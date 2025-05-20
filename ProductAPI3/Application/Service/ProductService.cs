using ProductAPI3.Application.Interfaces;
using ProductAPI3.Domain.Entities;

namespace ProductAPI3.Application.Service
{
    public class ProductService(IProductRepository repository) : IProductService
    {

        //public Task<IEnumerable<Product>> GetAllAsync() => repository.GetAllAsync();
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            //throw new Exception("Simulated exception from service layer");
             
            return await repository.GetAllAsync();  
        }

        public Task<Product?> GetByIdAsync(int id) => repository.GetByIdAsync(id);

        public Task<Product> CreateAsync(Product product) => repository.CreateAsync(product);

        public Task UpdateAsync(Product product) => repository.UpdateAsync(product);

        public Task DeleteAsync(int id) => repository.DeleteAsync(id);
    }
}
