using Application.DTOs;

namespace Application.Interfaces
{
    public interface IProductService
    {
        Task<bool> IsProductNameUniqueAsync(string name);
        Task<int> CreateProductAsync(ProductCreateDto dto, CancellationToken cancellationToken);
        Task<IEnumerable<ProductCreateDto>> GetAllProductsAsync(CancellationToken cancellationToken);
    }
}