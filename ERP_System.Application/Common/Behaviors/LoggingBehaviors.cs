using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Application.Common.Behaviors
{
    public class LoggingBehaviors<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly ILogger<LoggingBehaviors<TRequest, TResponse>> _logger;
        public LoggingBehaviors(ILogger<LoggingBehaviors<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request,
                                            RequestHandlerDelegate<TResponse> next,
                                            CancellationToken cancellationToken)
        {
            var reqName = typeof(TRequest).Name;
            _logger.LogInformation("-> Handling {reqName}", reqName);

            var response = await next();
            _logger.LogInformation("Handled {reqName}", reqName);

            return response;
        }
    }
}
