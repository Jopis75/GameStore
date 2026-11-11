using AutoMapper;
using FluentValidation;
using GameStore.Application.Dtos.General;
using GameStore.Application.Features.Consoles.Requests.Commands;
using GameStore.Application.Interfaces.Infrastructure;
using GameStore.Application.Interfaces.Persistance;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace GameStore.Application.Features.Consoles.RequestHandlers.Commands
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

                if (!validationResult.IsValid)
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
