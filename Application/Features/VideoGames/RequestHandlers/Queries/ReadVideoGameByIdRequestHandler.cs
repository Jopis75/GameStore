using Application.Dtos.General;
using Application.Features.VideoGames.Requests.Queries;
using Application.Interfaces.Persistance;
using Domain.Dtos;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.VideoGames.RequestHandlers.Queries
{
    public class ReadVideoGameByIdRequestHandler : IRequestHandler<ReadVideoGameByIdRequest, HttpResponseDto<VideoGameDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IValidator<ReadVideoGameByIdRequest> _validator;

        private readonly ILogger<ReadVideoGameByIdRequestHandler> _logger;

        public ReadVideoGameByIdRequestHandler(IUnitOfWork unitOfWork, IValidator<ReadVideoGameByIdRequest> validator, ILogger<ReadVideoGameByIdRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<VideoGameDto>> Handle(ReadVideoGameByIdRequest readVideoGameByIdRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin ReadVideoGameById {@ReadVideoGameByIdRequest}.", readVideoGameByIdRequest);

                cancellationToken.ThrowIfCancellationRequested();

                if (readVideoGameByIdRequest == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<VideoGameDto>(new ArgumentNullException(nameof(readVideoGameByIdRequest)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error ReadVideoGameById {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(readVideoGameByIdRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var httpResponseDto1 = new HttpResponseDto<VideoGameDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error ReadVideoGameById {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var videoGameDto = await _unitOfWork.VideoGameRepository.ReadByIdAsync(readVideoGameByIdRequest.Id, cancellationToken);

                var httpResponseDto = new HttpResponseDto<VideoGameDto>(videoGameDto, StatusCodes.Status200OK);
                _logger.LogInformation("Done ReadVideoGameById {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<VideoGameDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Canceled ReadVideoGameById {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<VideoGameDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error ReadVideoGameById {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
