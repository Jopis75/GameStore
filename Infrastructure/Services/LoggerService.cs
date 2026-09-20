using Application.Dtos.General;
using Application.Interfaces.Infrastructure;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services
{
    public class LoggerService<TCategoryName>(ILogger<TCategoryName> logger) : ILoggerService<TCategoryName>
    {
        public HttpResponseDto<TRequest> LogArgumentNullException<TRequest>(ArgumentNullException ex, string methodName)
            where TRequest : IRequest<HttpResponseDto<TRequest>>, new()
        {
            var httpResponseDto = new HttpResponseDto<TRequest>(ex.Message, StatusCodes.Status400BadRequest);
            logger.LogError(ex, $"Error {methodName}" + " {@HttpResponseDto}.", methodName, httpResponseDto);
            return httpResponseDto;
        }

        public void LogBeginInformation<TRequest>(string methodName, TRequest request)
            where TRequest : IRequest<HttpResponseDto<TRequest>>, new()
        {
            logger.LogInformation($"Begin {methodName}" + " {@" + $"{typeof(TRequest).Name}" + "}.", request);
        }

        public HttpResponseDto<TRequest> LogDoneInformation<TRequest>(string methodName, TRequest request, int statusCode)
            where TRequest : IRequest<HttpResponseDto<TRequest>>, new()
        {
            var httpResponseDto = new HttpResponseDto<TRequest>(request, statusCode);
            logger.LogInformation($"Done {methodName}" + " {@HttpResponseDto}.", methodName, httpResponseDto);
            return httpResponseDto;
        }

        public HttpResponseDto<TRequest> LogException<TRequest>(Exception ex, string methodName)
            where TRequest : IRequest<HttpResponseDto<TRequest>>, new()
        {
            var httpResponseDto = new HttpResponseDto<TRequest>(ex.Message, StatusCodes.Status500InternalServerError);
            logger.LogError(ex, $"Error {methodName}" + " {@HttpResponseDto}.", methodName, httpResponseDto);
            return httpResponseDto;
        }

        public HttpResponseDto<TRequest> LogOperationCanceledException<TRequest>(OperationCanceledException ex, string methodName)
            where TRequest : IRequest<HttpResponseDto<TRequest>>, new()
        {
            var httpResponseDto = new HttpResponseDto<TRequest>(ex.Message, StatusCodes.Status500InternalServerError);
            logger.LogError(ex, $"Canceled {methodName}" + " {@HttpResponseDto}.", methodName, httpResponseDto);
            return httpResponseDto;
        }

        public HttpResponseDto<TRequest> LogValidationException<TRequest>(ValidationException ex, string methodName)
            where TRequest : IRequest<HttpResponseDto<TRequest>>, new()
        {
            var httpResponseDto = new HttpResponseDto<TRequest>(ex.Message, StatusCodes.Status400BadRequest);
            logger.LogError(ex, $"Error {methodName}" + " {@HttpResponseDto}.", methodName, httpResponseDto);
            return httpResponseDto;
        }
    }
}
