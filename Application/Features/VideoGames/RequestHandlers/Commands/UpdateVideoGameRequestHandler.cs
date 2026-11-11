using AutoMapper;
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
    public class UpdateVideoGameRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<UpdateVideoGameRequest> validator, ILogger<UpdateVideoGameRequestHandler> logger) : IRequestHandler<UpdateVideoGameRequest, HttpResponseDto<VideoGameDto>>
    {
        public async Task<HttpResponseDto<VideoGameDto>> Handle(UpdateVideoGameRequest updateVideoGameRequest, CancellationToken cancellationToken)
        {
            await unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                logger.LogInformation("Begin UpdateVideoGame {@UpdateVideoGameRequest}.", updateVideoGameRequest);

                if (updateVideoGameRequest == null)
                {
                    var ex = new ArgumentNullException(nameof(updateVideoGameRequest));
                    var httpResponseDto1 = new HttpResponseDto<VideoGameDto>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error UpdateVideoGame {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await validator.ValidateAsync(updateVideoGameRequest, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var ex = new ValidationException(validationResult.Errors);
                    var httpResponseDto1 = new HttpResponseDto<VideoGameDto>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error UpdateVideoGame {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var videoGameDto = mapper.Map<VideoGameDto>(updateVideoGameRequest);
                var updatedVideoGameDto = await unitOfWork.VideoGameRepository.UpdateAsync(videoGameDto, cancellationToken);

                await unitOfWork.CommitTransactionAsync(cancellationToken);

                var httpResponseDto = new HttpResponseDto<VideoGameDto>(updatedVideoGameDto, StatusCodes.Status200OK);
                logger.LogInformation("Done UpdateVideoGame {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                await unitOfWork.RollbackTransactionAsync(cancellationToken);

                var httpResponseDto1 = new HttpResponseDto<VideoGameDto>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Canceled UpdateVideoGame {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackTransactionAsync(cancellationToken);

                var httpResponseDto1 = new HttpResponseDto<VideoGameDto>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Error UpdateVideoGame {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
