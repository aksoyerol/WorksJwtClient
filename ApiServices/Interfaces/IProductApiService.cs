using System.Collections.Generic;
using System.Threading.Tasks;
using WorksJwtClient.Models;

namespace WorksJwtClient.ApiServices.Interfaces{
    public interface IProductApiService{
        Task<List<ProductList>> GetAllAsync();
        Task AddAsync(ProductAdd productAdd);
        Task<ProductList> GetByIdAsync(int id);
    }
}