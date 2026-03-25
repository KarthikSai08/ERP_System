using ERP_System.Application.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace ERP_System.Infrastructure.Services
{
    public class RedisCacheService : ICacheService
    {
        private readonly IDistributedCache _cache;
        public RedisCacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<T?> GetAsync<T>(string key, CancellationToken ct)
        {
            var data = await _cache.GetStringAsync(key, ct);

            return data == null ? default : JsonSerializer.Deserialize<T>(data);
        }

        public async Task SetAsync<T>(string key, T value,TimeSpan expiration, CancellationToken ct)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration
            };

            var json = JsonSerializer.Serialize(value);
            await _cache.SetStringAsync(key, json, options, ct);
        }

        public async Task RemoveAsync(string key, CancellationToken ct)
        {
            await _cache.RemoveAsync(key, ct);
        }
        public async Task InvalidateGroupAsync(string groupKey, CancellationToken ct)
        {
            var version = await GetAsync<int>(groupKey, ct);
            if (version == 0) version = 1;
            await SetAsync(groupKey, version + 1, TimeSpan.FromDays(30), ct);
        }
    }
}
