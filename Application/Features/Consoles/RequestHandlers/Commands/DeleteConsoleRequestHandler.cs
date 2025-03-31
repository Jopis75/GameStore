using Application.Dtos.General;
using Application.Features.Consoles.Requests.Commands;
using Application.Interfaces.Persistance;
using Domain.Dtos;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Consoles.RequestHandlers.Commands
{
    public class DeleteConsoleRequestHandler(IUnitOfWork unitOfWork, IValidator<DeleteConsoleRequest> validator, ILogger<DeleteConsoleRequestHandler> logger) : IRequestHandler<DeleteConsoleRequest, HttpResponseDto<ConsoleDto>>
    {
        public async Task<HttpResponseDto<ConsoleDto>> Handle(DeleteConsoleRequest deleteConsoleRequest, CancellationToken cancellationToken)
        {
            await unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                logger.LogInformation("Begin DeleteConsole {@DeleteConsoleRequest}.", deleteConsoleRequest);

                if (deleteConsoleRequest == null)
                {
                    var ex = new ArgumentNullException(nameof(deleteConsoleRequest));
                    var httpResponseDto1 = new HttpResponseDto<ConsoleDto>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error DeleteConsole {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await validator.ValidateAsync(deleteConsoleRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var ex = new ValidationException(validationResult.Errors);
                    var httpResponseDto1 = new HttpResponseDto<ConsoleDto>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error DeleteConsole {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var deletedConsoleDto = await unitOfWork.ConsoleRepository.DeleteByIdAsync(deleteConsoleRequest.Id, cancellationToken);

                await unitOfWork.CommitTransactionAsync(cancellationToken);

                var httpResponseDto = new HttpResponseDto<ConsoleDto>(deletedConsoleDto, StatusCodes.Status200OK);
                logger.LogInformation("Done DeleteConsole {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                await unitOfWork.RollbackTransactionAsync(cancellationToken);

                var httpResponseDto1 = new HttpResponseDto<ConsoleDto>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Canceled DeleteConsole {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackTransactionAsync(cancellationToken);

                var httpResponseDto1 = new HttpResponseDto<ConsoleDto>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Error DeleteConsole {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
