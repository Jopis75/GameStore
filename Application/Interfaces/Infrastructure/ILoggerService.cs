using Application.Dtos.General;
using FluentValidation;
using MediatR;

namespace Application.Interfaces.Infrastructure
{
    public interface ILoggerService<TCategoryName>
    {
        HttpResponseDto<TRequest> LogArgumentNullException<TRequest>(ArgumentNullException ex, string methodName)
            where TRequest : IRequest<HttpResponseDto<TRequest>>, new();

        void LogBeginInformation<TRequest>(string methodName, TRequest request)
            where TRequest : IRequest<HttpResponseDto<TRequest>>, new();

        HttpResponseDto<TRequest> LogDoneInformation<TRequest>(string methodName, TRequest request, int statusCode)
            where TRequest : IRequest<HttpResponseDto<TRequest>>, new();

        HttpResponseDto<TRequest> LogException<TRequest>(Exception ex, string methodName)
            where TRequest : IRequest<HttpResponseDto<TRequest>>, new();

        HttpResponseDto<TRequest> LogOperationCanceledException<TRequest>(OperationCanceledException ex, string methodName)
            where TRequest : IRequest<HttpResponseDto<TRequest>>, new();

        HttpResponseDto<TRequest> LogValidationException<TRequest>(ValidationException ex, string methodName)
            where TRequest : IRequest<HttpResponseDto<TRequest>>, new();
    }
}
