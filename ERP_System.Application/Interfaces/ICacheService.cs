using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Application.Interfaces
{
    public interface ICacheService
    {
        Task<T?> GetAsync<T>(string key, CancellationToken ct);
        Task SetAsync<T>(string key, T value,TimeSpan expiration, CancellationToken ct);
        Task RemoveAsync(string key, CancellationToken ct);

        Task InvalidateGroupAsync(string groupKey, CancellationToken ct);

    }
}
