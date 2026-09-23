using Application.Dtos.General;
using Application.Events;
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
            logger.LogError(ex, $"Error {methodName}" + " {@HttpResponseDto}.", httpResponseDto);
            return httpResponseDto;
        }

        public void LogBeginInformation<TData>(string methodName, TData data)
            where TData : class, new()
        {
            logger.LogInformation($"Begin {methodName}" + " {@" + $"{typeof(TData).Name}" + "}.", data);
        }

        public HttpResponseDto<TRequest> LogDoneInformation<TRequest>(string methodName, TRequest request, int statusCode)
            where TRequest : IRequest<HttpResponseDto<TRequest>>, new()
        {
            var httpResponseDto = new HttpResponseDto<TRequest>(request, statusCode);
            logger.LogInformation($"Done {methodName}" + " {@HttpResponseDto}.", httpResponseDto);
            return httpResponseDto;
        }

        public void LogDoneInformation<TData>(string methodName, TData data)
            where TData : class, new()
        {
            logger.LogInformation($"Done {methodName}" + " {@" + $"{typeof(TData).Name}" + "}.", data);
        }

        public HttpResponseDto<TRequest> LogException<TRequest>(Exception ex, string methodName)
            where TRequest : IRequest<HttpResponseDto<TRequest>>, new()
        {
            var httpResponseDto = new HttpResponseDto<TRequest>(ex.Message, StatusCodes.Status500InternalServerError);
            logger.LogError(ex, $"Error {methodName}" + " {@HttpResponseDto}.", httpResponseDto);
            return httpResponseDto;
        }

        public void LogException<TData>(Exception ex, string methodName, TData data)
            where TData : class, new()
        {
            logger.LogError(ex, $"Error {methodName}" + " {@" + $"{typeof(TData).Name}" + "}.", data);
        }

        public HttpResponseDto<TRequest> LogOperationCanceledException<TRequest>(OperationCanceledException ex, string methodName)
            where TRequest : IRequest<HttpResponseDto<TRequest>>, new()
        {
            var httpResponseDto = new HttpResponseDto<TRequest>(ex.Message, StatusCodes.Status500InternalServerError);
            logger.LogError(ex, $"Canceled {methodName}" + " {@HttpResponseDto}.", httpResponseDto);
            return httpResponseDto;
        }

        public void LogOperationCanceledException<TData>(OperationCanceledException ex, string methodName, TData data)
            where TData : class, new()
        {
            logger.LogError(ex, $"Canceled {methodName}" + " {@" + $"{typeof(TData).Name}" + "}.", data);
        }

        public HttpResponseDto<TRequest> LogValidationException<TRequest>(ValidationException ex, string methodName)
            where TRequest : IRequest<HttpResponseDto<TRequest>>, new()
        {
            var httpResponseDto = new HttpResponseDto<TRequest>(ex.Message, StatusCodes.Status400BadRequest);
            logger.LogError(ex, $"Error {methodName}" + " {@HttpResponseDto}.", httpResponseDto);
            return httpResponseDto;
        }
    }
}
