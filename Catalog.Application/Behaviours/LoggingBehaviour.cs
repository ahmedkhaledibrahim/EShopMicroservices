using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Behaviours
{
    public sealed class LoggingBehaviour<TRequest, TResponse>(ILogger<LoggingBehaviour<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : class
    {
        private readonly ILogger<LoggingBehaviour<TRequest, TResponse>> _logger = logger;
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling {RequestName} with content: {@Request}", typeof(TRequest).Name, request);
            var timer = System.Diagnostics.Stopwatch.StartNew();
            var response = await next();
            timer.Stop();
            _logger.LogInformation("Handled {RequestName} in {ElapsedMilliseconds}ms with response: {@Response}", typeof(TRequest).Name, timer.ElapsedMilliseconds, response);
            return response;
        }
    }
}
