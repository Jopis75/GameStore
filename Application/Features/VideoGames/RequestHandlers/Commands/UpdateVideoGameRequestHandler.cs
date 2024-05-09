using Application.Dtos.Common;
using Application.Dtos.VideoGames;
using Application.Features.VideoGames.Requests.Commands;
using Application.Interfaces.Persistance;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.VideoGames.RequestHandlers.Commands
{
    public class UpdateVideoGameRequestHandler : IRequestHandler<UpdateVideoGameRequest, HttpResponseDto<UpdateVideoGameResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IValidator<UpdateVideoGameRequestDto> _validator;

        private readonly ILogger<UpdateVideoGameRequestHandler> _logger;

        public UpdateVideoGameRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<UpdateVideoGameRequestDto> validator, ILogger<UpdateVideoGameRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<UpdateVideoGameResponseDto>> Handle(UpdateVideoGameRequest updateVideoGameRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin UpdateVideoGame {@UpdateVideoGameRequest}.", updateVideoGameRequest);

                cancellationToken.ThrowIfCancellationRequested();

                if (updateVideoGameRequest.UpdateVideoGameRequestDto == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<UpdateVideoGameResponseDto>(new ArgumentNullException(nameof(updateVideoGameRequest.UpdateVideoGameRequestDto)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error UpdateVideoGame {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(updateVideoGameRequest.UpdateVideoGameRequestDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var httpResponseDto1 = new HttpResponseDto<UpdateVideoGameResponseDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error UpdateVideoGame {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var videoGame = await _unitOfWork.VideoGameRepository.ReadByIdAsync(updateVideoGameRequest.UpdateVideoGameRequestDto.Id);
                _mapper.Map(updateVideoGameRequest.UpdateVideoGameRequestDto, videoGame);
                var updatedVideoGame = await _unitOfWork.VideoGameRepository.UpdateAsync(videoGame);
                await _unitOfWork.SaveAsync();

                var httpResponseDto = new HttpResponseDto<UpdateVideoGameResponseDto>(new UpdateVideoGameResponseDto
                {
                    Id = updatedVideoGame.Id,
                    UpdatedAt = updatedVideoGame.UpdatedAt,
                    UpdatedBy = updatedVideoGame.UpdatedBy
                }, StatusCodes.Status200OK);
                _logger.LogInformation("Done UpdateVideoGame {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<UpdateVideoGameResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Canceled UpdateVideoGame {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<UpdateVideoGameResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error UpdateVideoGame {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
