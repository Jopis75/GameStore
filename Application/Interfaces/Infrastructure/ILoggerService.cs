using Application.Dtos.General;
using Application.Events;
using FluentValidation;
using MediatR;

namespace Application.Interfaces.Infrastructure
{
    public interface ILoggerService<TCategoryName>
    {
        HttpResponseDto<TRequest> LogArgumentNullException<TRequest>(ArgumentNullException ex, string methodName)
            where TRequest : IRequest<HttpResponseDto<TRequest>>, new();

        void LogBeginInformation<TData>(string methodName, TData data)
            where TData : class, new();

        HttpResponseDto<TRequest> LogDoneInformation<TRequest>(string methodName, TRequest request, int statusCode)
            where TRequest : IRequest<HttpResponseDto<TRequest>>, new();

        void LogDoneInformation<TData>(string methodName, TData data)
            where TData : class, new();

        HttpResponseDto<TRequest> LogException<TRequest>(Exception ex, string methodName)
            where TRequest : IRequest<HttpResponseDto<TRequest>>, new();

        void LogException<TData>(Exception ex, string methodName, TData data)
            where TData : class, new();

        HttpResponseDto<TRequest> LogOperationCanceledException<TRequest>(OperationCanceledException ex, string methodName)
            where TRequest : IRequest<HttpResponseDto<TRequest>>, new();

        HttpResponseDto<TRequest> LogValidationException<TRequest>(ValidationException ex, string methodName)
            where TRequest : IRequest<HttpResponseDto<TRequest>>, new();

        void LogOperationCanceledException<TData>(OperationCanceledException ex, string methodName, TData data)
            where TData : class, new();
    }
}
