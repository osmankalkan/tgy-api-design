using ProjectApi.DTOs;

namespace ProjectApi.Services
{
    public interface IProductServices
    {
        Task<List<ProductResponse>> GetAllAsync();
        Task<ProductResponse> GetByIdAsync(int id);

        Task<ProductResponse> CreateAsync(CreateProductRequest request);
        Task<bool> UpdateAsync(int id, UpdateProductRequest request);
        Task<bool> DeleteAsync(int id);
    }
}