using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BuildingBlocks.Behaviors;

public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull, IRequest<TResponse> where TResponse : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        logger.LogInformation("[START] - Handling request = {Request} -- response = {Response} -- requestData = {requestData}",
            typeof(TRequest).Name, typeof(TResponse).Name, request);

        var timer = new Stopwatch();
        timer.Start();

        var response = await next();
        timer.Stop();

        var timeTaken = timer.Elapsed;

        if (timeTaken.Seconds > 5)
        {
            logger.LogWarning("[SLOW] - Handling request = {Request} -- response = {Response} -- requestData = {requestData} -- timeTaken = {timeTaken}",
                typeof(TRequest).Name, typeof(TResponse).Name, request, timeTaken);
        }

        logger.LogInformation("[END] - Handling request = {Request} -- response = {Response} -- requestData = {requestData} -- timeTaken = {timeTaken}",
            typeof(TRequest).Name, typeof(TResponse).Name, request, timeTaken);

        return response;
    }
}

