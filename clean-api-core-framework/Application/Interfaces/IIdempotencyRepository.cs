using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IIdempotencyRepository
    {
        Task<string?> GetResponseAsync(string key);
        Task SaveResponseAsync(string key, string response, CancellationToken cancellationToken);
        Task CleanUpExpiredAsync();
    }
}