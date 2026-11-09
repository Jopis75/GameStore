using Application.Dtos.General;
using Application.Features.Consoles.Requests.Commands;
using Application.Interfaces.Infrastructure;
using Application.Interfaces.Persistance;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Consoles.RequestHandlers.Commands
{
    public class CreateConsoleRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<CreateConsoleRequest> validator, ILoggerService<CreateConsoleRequestHandler> loggerService) : IRequestHandler<CreateConsoleRequest, HttpResponseDto<CreateConsoleRequest>>
    {
        public async Task<HttpResponseDto<CreateConsoleRequest>> Handle(CreateConsoleRequest createConsoleRequest, CancellationToken cancellationToken)
        {
            var methodName = "HandleCreateConsoleRequest";
            var consoleEventsTopicEnvironmentVariable = "CONSOLE_EVENTS_TOPIC";

            try
            {
                loggerService.LogBeginInformation(methodName, createConsoleRequest);

                if (createConsoleRequest == null)
                {
                    return loggerService.LogArgumentNullException<CreateConsoleRequest>(new ArgumentNullException(nameof(createConsoleRequest)), methodName);
                }

                var validationResult = await validator.ValidateAsync(createConsoleRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    return loggerService.LogValidationException<CreateConsoleRequest>(new ValidationException(validationResult.Errors), methodName);
                }

                

                return loggerService.LogDoneInformation(methodName, createConsoleRequest, StatusCodes.Status201Created);
            }
            catch (OperationCanceledException ex)
            {
                return loggerService.LogOperationCanceledException<CreateConsoleRequest>(ex, methodName);
            }
            catch (Exception ex)
            {
                return loggerService.LogException<CreateConsoleRequest>(ex, methodName);
            }
        }
    }
}
