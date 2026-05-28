using Application.Dtos.General;
using Application.Exceptions;
using Application.Features.VideoGames.Requests.Queries;
using Application.Interfaces.Persistance;
using Domain.Dtos;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.VideoGames.RequestHandlers.Queries
{
    public class ReadVideoGameByIdRequestHandler(IUnitOfWork unitOfWork, IValidator<ReadVideoGameByIdRequest> validator, ILogger<ReadVideoGameByIdRequestHandler> logger) : IRequestHandler<ReadVideoGameByIdRequest, HttpResponseDto<VideoGameDto>>
    {
        public async Task<HttpResponseDto<VideoGameDto>> Handle(ReadVideoGameByIdRequest readVideoGameByIdRequest, CancellationToken cancellationToken)
        {
            try
            {
                logger.LogInformation("Begin ReadVideoGameById {@ReadVideoGameByIdRequest}.", readVideoGameByIdRequest);

                if (readVideoGameByIdRequest == null)
                {
                    var ex = new ArgumentNullException(nameof(readVideoGameByIdRequest));
                    var httpResponseDto1 = new HttpResponseDto<VideoGameDto>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error ReadVideoGameById {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await validator.ValidateAsync(readVideoGameByIdRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var ex = new ValidationException(validationResult.Errors);
                    var httpResponseDto1 = new HttpResponseDto<VideoGameDto>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error ReadVideoGameById {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var videoGameDto = await unitOfWork.VideoGameRepository.ReadByIdAsync(readVideoGameByIdRequest.Id, cancellationToken);

                if (videoGameDto.IsNullObject)
                {
                    var ex = new NotFoundException($"Could not find VideoGame object with Id {readVideoGameByIdRequest.Id}.");
                    var httpResponseDto1 = new HttpResponseDto<VideoGameDto>(ex.Message, StatusCodes.Status404NotFound);
                    logger.LogError(ex, "Error ReadVideoGameById {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var httpResponseDto = new HttpResponseDto<VideoGameDto>(videoGameDto, StatusCodes.Status200OK);
                logger.LogInformation("Done ReadVideoGameById {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<VideoGameDto>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Canceled ReadVideoGameById {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<VideoGameDto>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Error ReadVideoGameById {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
