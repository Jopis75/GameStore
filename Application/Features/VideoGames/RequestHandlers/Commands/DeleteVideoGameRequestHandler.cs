using FluentValidation;
using GameStore.Application.Dtos.General;
using GameStore.Application.Features.VideoGames.Requests.Commands;
using GameStore.Application.Interfaces.Persistance;
using GameStore.Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace GameStore.Application.Features.VideoGames.RequestHandlers.Commands
{
    public class DeleteVideoGameRequestHandler(IUnitOfWork unitOfWork, IValidator<DeleteVideoGameRequest> validator, ILogger<DeleteVideoGameRequestHandler> logger) : IRequestHandler<DeleteVideoGameRequest, HttpResponseDto<VideoGameDto>>
    {
        public async Task<HttpResponseDto<VideoGameDto>> Handle(DeleteVideoGameRequest deleteVideoGameRequest, CancellationToken cancellationToken)
        {
            await unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                logger.LogInformation("Begin DeleteVideoGame {@DeleteVideoGameRequest}.", deleteVideoGameRequest);

                if (deleteVideoGameRequest == null)
                {
                    var ex = new ArgumentNullException(nameof(deleteVideoGameRequest));
                    var httpResponseDto1 = new HttpResponseDto<VideoGameDto>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error DeleteVideoGame {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await validator.ValidateAsync(deleteVideoGameRequest, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var ex = new ValidationException(validationResult.Errors);
                    var httpResponseDto1 = new HttpResponseDto<VideoGameDto>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error DeleteVideoGame {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var deletedVideoGameDto = await unitOfWork.VideoGameRepository.DeleteByIdAsync(deleteVideoGameRequest.Id, cancellationToken);

                await unitOfWork.CommitTransactionAsync(cancellationToken);

                var httpResponseDto = new HttpResponseDto<VideoGameDto>(deletedVideoGameDto, StatusCodes.Status200OK);
                logger.LogInformation("Done DeleteVideoGame {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                await unitOfWork.RollbackTransactionAsync(cancellationToken);

                var httpResponseDto1 = new HttpResponseDto<VideoGameDto>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Canceled DeleteVideoGame {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackTransactionAsync(cancellationToken);

                var httpResponseDto1 = new HttpResponseDto<VideoGameDto>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Error DeleteVideoGame {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
