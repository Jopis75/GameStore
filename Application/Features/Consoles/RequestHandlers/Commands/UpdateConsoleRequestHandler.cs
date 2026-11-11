using AutoMapper;
using FluentValidation;
using GameStore.Application.Dtos.General;
using GameStore.Application.Features.Consoles.Requests.Commands;
using GameStore.Application.Interfaces.Persistance;
using GameStore.Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace GameStore.Application.Features.Consoles.RequestHandlers.Commands
{
    public class UpdateConsoleRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<UpdateConsoleRequest> validator, ILogger<UpdateConsoleRequestHandler> logger) : IRequestHandler<UpdateConsoleRequest, HttpResponseDto<ConsoleDto>>
    {
        public async Task<HttpResponseDto<ConsoleDto>> Handle(UpdateConsoleRequest updateConsoleRequest, CancellationToken cancellationToken)
        {
            await unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                logger.LogInformation("Begin UpdateConsole {@UpdateConsoleRequest}.", updateConsoleRequest);

                if (updateConsoleRequest == null)
                {
                    var ex = new ArgumentNullException(nameof(updateConsoleRequest));
                    var httpResponseDto1 = new HttpResponseDto<ConsoleDto>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error UpdateConsole {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await validator.ValidateAsync(updateConsoleRequest, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var ex = new ValidationException(validationResult.Errors);
                    var httpResponseDto1 = new HttpResponseDto<ConsoleDto>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error UpdateConsole {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var consoleDto = mapper.Map<ConsoleDto>(updateConsoleRequest);
                var updatedConsoleDto = await unitOfWork.ConsoleRepository.UpdateAsync(consoleDto, cancellationToken);

                await unitOfWork.CommitTransactionAsync(cancellationToken);

                var httpResponseDto = new HttpResponseDto<ConsoleDto>(updatedConsoleDto, StatusCodes.Status200OK);
                logger.LogInformation("Done UpdateConsole {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                await unitOfWork.RollbackTransactionAsync(cancellationToken);

                var httpResponseDto1 = new HttpResponseDto<ConsoleDto>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Canceled UpdateConsole {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackTransactionAsync(cancellationToken);

                var httpResponseDto1 = new HttpResponseDto<ConsoleDto>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Error UpdateConsole {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
