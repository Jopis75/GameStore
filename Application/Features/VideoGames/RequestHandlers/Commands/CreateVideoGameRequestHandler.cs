using Application.Dtos.Common;
using Application.Features.VideoGames.Requests.Commands;
using Application.Interfaces.Persistance;
using Domain.Dtos;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.VideoGames.RequestHandlers.Commands
{
    public class CreateVideoGameRequestHandler : IRequestHandler<CreateVideoGameRequest, HttpResponseDto<VideoGameDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IValidator<VideoGameDto> _validator;

        private readonly ILogger<CreateVideoGameRequestHandler> _logger;

        public CreateVideoGameRequestHandler(IUnitOfWork unitOfWork, IValidator<VideoGameDto> validator, ILogger<CreateVideoGameRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<VideoGameDto>> Handle(CreateVideoGameRequest createVideoGameRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin CreateVideoGame {@CreateVideoGameRequest}.", createVideoGameRequest);

                cancellationToken.ThrowIfCancellationRequested();

                if (createVideoGameRequest.VideoGameDto == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<VideoGameDto>(new ArgumentNullException(nameof(createVideoGameRequest.VideoGameDto)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error CreateVideoGame {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(createVideoGameRequest.VideoGameDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var httpResponseDto1 = new HttpResponseDto<VideoGameDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error CreateVideoGame {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var createdVideoGameDto = await _unitOfWork.VideoGameRepository.CreateAsync(createVideoGameRequest.VideoGameDto);
                await _unitOfWork.SaveAsync();

                var httpResponseDto = new HttpResponseDto<VideoGameDto>(createdVideoGameDto, StatusCodes.Status201Created);
                _logger.LogInformation("Done CreateVideoGame {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<VideoGameDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Canceled CreateVideoGame {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<VideoGameDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error CreateVideoGame {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
