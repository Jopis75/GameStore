using Application.Dtos.General;
using Application.Features.Consoles.Requests.Queries;
using Application.Interfaces.Persistance;
using Domain.Dtos;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Consoles.RequestHandlers.Queries
{
    public class ReadConsoleByIdRequestHandler(IUnitOfWork unitOfWork, IValidator<ReadConsoleByIdRequest> validator, ILogger<ReadConsoleByIdRequestHandler> logger) : IRequestHandler<ReadConsoleByIdRequest, HttpResponseDto<ConsoleDto>>
    {
        public async Task<HttpResponseDto<ConsoleDto>> Handle(ReadConsoleByIdRequest readConsoleByIdRequest, CancellationToken cancellationToken)
        {
            try
            {
                logger.LogInformation("Begin ReadConsoleById {@ReadConsoleByIdRequest}.", readConsoleByIdRequest);

                if (readConsoleByIdRequest == null)
                {
                    var ex = new ArgumentNullException(nameof(readConsoleByIdRequest));
                    var httpResponseDto1 = new HttpResponseDto<ConsoleDto>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error ReadConsoleById {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await validator.ValidateAsync(readConsoleByIdRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var ex = new ValidationException(validationResult.Errors);
                    var httpResponseDto1 = new HttpResponseDto<ConsoleDto>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error ReadConsoleById {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var consoleDto = await unitOfWork.ConsoleRepository.ReadByIdAsync(readConsoleByIdRequest.Id, cancellationToken);

                var httpResponseDto = new HttpResponseDto<ConsoleDto>(consoleDto, StatusCodes.Status200OK);
                logger.LogInformation("Done ReadConsoleById {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ConsoleDto>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Canceled ReadConsoleById {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ConsoleDto>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Error ReadConsoleById {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
