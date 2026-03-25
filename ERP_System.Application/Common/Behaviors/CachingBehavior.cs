using ERP_System.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Application.Common.Behaviors
{
    public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ICacheService _cache;
        private readonly ILogger<CachingBehavior<TRequest, TResponse>> _logger;
        public CachingBehavior(ICacheService cache, ILogger<CachingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
            _cache = cache;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken ct)
        {
            if (request is not ICacheableQuery cacheable)
                return await next();

            var cached = await _cache.GetAsync<TResponse>(cacheable.CacheKey, ct);

            if (cached != null)
            {
                _logger.LogInformation("Cache Hit!! → {Key} ", cacheable.CacheKey);
                return cached;
            }
            _logger.LogInformation("Cache Misss!!!! → {Key} ", cacheable.CacheKey);
            var response = await next();

            await _cache.SetAsync(cacheable.CacheKey, response, cacheable.CacheExpiration, ct);

            return response;
        }
    }
}
