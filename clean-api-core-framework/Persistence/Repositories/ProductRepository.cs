using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IApplicationDbContext _context;

        public ProductRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        // Check if the product name is unique
        public async Task<bool> IsProductNameUniqueAsync(string productName)
        {
            // Check if any product with the same name exists in the database
            var productExisit = await _context.Products.AnyAsync(p => p.Name.Equals(p.Name.ToLower() == productName.ToLower()));
            return !productExisit;

        }

        // Get all products
        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        // Get a single product by ID
        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<int> AddAsync(Product product, CancellationToken cancellationToken)
        {
            if (!File.Exists(product.ImageTempPath))
                throw new FileNotFoundException("Image temp file not found", product.ImageTempPath);

            // 300 KB , make htis configurable if needed and make it resuable as we have used in multiple places
            const int chunkSize = 300 * 1024;
            // Using async true will clear up the memory when the job done
            using var inputStream = new FileStream(product.ImageTempPath, FileMode.Open, FileAccess.Read, FileShare.Read, chunkSize, useAsync: true);
            using (var memoryStream = new MemoryStream())
            {
                var buffer = new byte[chunkSize];
                int bytesRead;

                while ((bytesRead = await inputStream.ReadAsync(buffer.AsMemory(0, chunkSize), cancellationToken)) > 0)
                {
                    await memoryStream.WriteAsync(buffer.AsMemory(0, bytesRead), cancellationToken);
                }

                product.ImageData = memoryStream.ToArray();
            }

            _context.Products.Add(product);
            var result = await _context.SaveChangesAsync(cancellationToken);
            return result;
        }

        public async Task<bool> IsProductNameUniqueAsync(string name, CancellationToken cancellationToken)
        {
            return !await _context.Products.AnyAsync(p => p.Name == name, cancellationToken);
        }

        // Update an existing product
        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
        }

        // Delete a product
        public async Task DeleteAsync(Product product)
        {
            _context.Products.Remove(product);
        }
    }
}