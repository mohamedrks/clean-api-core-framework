using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace Persistence.Repositories
{
    public class IdempotencyRepository : IIdempotencyRepository
    {
        private readonly IApplicationDbContext _context;

        public IdempotencyRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public Task CleanUpExpiredAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<string?> GetResponseAsync(string key)
        {
            var idempotencyKey = await _context.IdempotencyKeys.FirstOrDefaultAsync(p => p.RequestHash == key);
            return idempotencyKey?.RequestHash;
        }

        public async Task SaveResponseAsync(string key, string response, CancellationToken cancellationToken)
        {
            // Make this more clean according to the architecure

            IdempotencyKey idempotencyKey = new IdempotencyKey
            {
                RequestHash = key,
                ResponseBody = response,
                CreatedAt = DateTime.UtcNow,
                ExpiryAt = DateTime.UtcNow.AddDays(3) // Set the expiration time as needed
            };

            _context.IdempotencyKeys.Add(idempotencyKey);
            var result = await _context.SaveChangesAsync(cancellationToken);
        }
    }
}