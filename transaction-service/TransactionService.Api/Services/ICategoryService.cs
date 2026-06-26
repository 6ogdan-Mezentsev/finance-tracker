using TransactionService.Api.Dtos;

namespace TransactionService.Api.Services;

public interface ICategoryService
{
    Task<CategoryResponse> CreateAsync(CreateCategoryRequest request);

    Task<List<CategoryResponse>> GetByUserAsync(int userId);

    Task<CategoryResponse?> GetByIdAsync(int id, int userId);

    Task<CategoryResponse?> UpdateAsync(int id, UpdateCategoryRequest request);

    Task<bool> DeleteAsync(int id, int userId);
}
