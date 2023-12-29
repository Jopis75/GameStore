using Application.Dtos.Common;
using Application.Dtos.VideoGames;
using Application.Features.VideoGames.Requests.Commands;
using Application.Interfaces.Persistance;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.VideoGames.RequestHandlers.Commands
{
    public class DeleteVideoGameRequestHandler : IRequestHandler<DeleteVideoGameRequest, HttpResponseDto<DeleteVideoGameResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IValidator<DeleteVideoGameRequestDto> _validator;

        private readonly ILogger<DeleteVideoGameRequestHandler> _logger;

        public DeleteVideoGameRequestHandler(IUnitOfWork unitOfWork, IValidator<DeleteVideoGameRequestDto> validator, ILogger<DeleteVideoGameRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<DeleteVideoGameResponseDto>> Handle(DeleteVideoGameRequest deleteVideoGameRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin DeleteVideoGame {@DeleteVideoGameRequest}.", deleteVideoGameRequest);

                if (deleteVideoGameRequest.DeleteVideoGameRequestDto == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<DeleteVideoGameResponseDto>(new ArgumentNullException(nameof(deleteVideoGameRequest.DeleteVideoGameRequestDto)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error DeleteVideoGame {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(deleteVideoGameRequest.DeleteVideoGameRequestDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var httpResponseDto1 = new HttpResponseDto<DeleteVideoGameResponseDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error DeleteVideoGame {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var videoGame = await _unitOfWork.VideoGameRepository.ReadByIdAsync(deleteVideoGameRequest.DeleteVideoGameRequestDto.Id);
                var deletedVideoGame = await _unitOfWork.VideoGameRepository.DeleteAsync(videoGame);
                await _unitOfWork.SaveAsync();

                var httpResponseDto = new HttpResponseDto<DeleteVideoGameResponseDto>(new DeleteVideoGameResponseDto
                {
                    Id = deletedVideoGame.Id,
                    DeletedAt = deletedVideoGame.DeletedAt,
                    DeletedBy = string.Empty
                }, StatusCodes.Status200OK);
                _logger.LogInformation("End DeleteVideoGame {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<DeleteVideoGameResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error DeleteVideoGame {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
