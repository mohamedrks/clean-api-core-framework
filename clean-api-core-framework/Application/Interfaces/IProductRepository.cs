using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProductRepository
    {
        Task<bool> IsProductNameUniqueAsync(string productName, CancellationToken cancellationToken);
        Task<List<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
        Task<int> AddAsync(Product product, CancellationToken cancellationToken);
        Task UpdateAsync(Product product);
        Task DeleteAsync(Product product);
    }
}