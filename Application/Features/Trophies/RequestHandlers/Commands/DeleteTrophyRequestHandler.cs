using FluentValidation;
using GameStore.Application.Dtos.General;
using GameStore.Application.Features.Trophies.Requests.Commands;
using GameStore.Application.Interfaces.Persistance;
using GameStore.Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace GameStore.Application.Features.Trophies.RequestHandlers.Commands
{
    public class DeleteTrophyRequestHandler(IUnitOfWork unitOfWork, IValidator<DeleteTrophyRequest> validator, ILogger<DeleteTrophyRequestHandler> logger) : IRequestHandler<DeleteTrophyRequest, HttpResponseDto<TrophyDto>>
    {
        public async Task<HttpResponseDto<TrophyDto>> Handle(DeleteTrophyRequest deleteTrophyRequest, CancellationToken cancellationToken)
        {
            await unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                logger.LogInformation("Begin DeleteTrophy {@DeleteTrophyRequest}.", deleteTrophyRequest);

                if (deleteTrophyRequest == null)
                {
                    var ex = new ArgumentNullException(nameof(deleteTrophyRequest));
                    var httpResponseDto1 = new HttpResponseDto<TrophyDto>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error DeleteTrophy {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await validator.ValidateAsync(deleteTrophyRequest, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var ex = new ValidationException(validationResult.Errors);
                    var httpResponseDto1 = new HttpResponseDto<TrophyDto>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error DeleteTrophy {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var deletedTrophyDto = await unitOfWork.TrophyRepository.DeleteByIdAsync(deleteTrophyRequest.Id, cancellationToken);

                await unitOfWork.CommitTransactionAsync(cancellationToken);

                var httpResponseDto = new HttpResponseDto<TrophyDto>(deletedTrophyDto, StatusCodes.Status200OK);
                logger.LogInformation("Done DeleteTrophy {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                await unitOfWork.RollbackTransactionAsync(cancellationToken);

                var httpResponseDto1 = new HttpResponseDto<TrophyDto>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Canceled DeleteTrophy {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackTransactionAsync(cancellationToken);

                var httpResponseDto1 = new HttpResponseDto<TrophyDto>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Error DeleteTrophy {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
