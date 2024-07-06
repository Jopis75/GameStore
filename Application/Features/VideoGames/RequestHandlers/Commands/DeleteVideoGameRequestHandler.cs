using Application.Dtos.General;
using Application.Features.VideoGames.Requests.Commands;
using Application.Interfaces.Persistance;
using Domain.Dtos;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.VideoGames.RequestHandlers.Commands
{
    public class DeleteVideoGameRequestHandler : IRequestHandler<DeleteVideoGameRequest, HttpResponseDto<VideoGameDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IValidator<VideoGameDto> _validator;

        private readonly ILogger<DeleteVideoGameRequestHandler> _logger;

        public DeleteVideoGameRequestHandler(IUnitOfWork unitOfWork, IValidator<VideoGameDto> validator, ILogger<DeleteVideoGameRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<VideoGameDto>> Handle(DeleteVideoGameRequest deleteVideoGameRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin DeleteVideoGame {@DeleteVideoGameRequest}.", deleteVideoGameRequest);

                cancellationToken.ThrowIfCancellationRequested();

                if (deleteVideoGameRequest.Id == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<VideoGameDto>(new ArgumentNullException(nameof(deleteVideoGameRequest.Id)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error DeleteVideoGame {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(deleteVideoGameRequest.Id, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var httpResponseDto1 = new HttpResponseDto<VideoGameDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error DeleteVideoGame {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var deleteVideoGameDto = await _unitOfWork.VideoGameRepository.DeleteByIdAsync(deleteVideoGameRequest.Id ?? 0);
                await _unitOfWork.SaveAsync();

                var httpResponseDto = new HttpResponseDto<VideoGameDto>(deleteVideoGameDto, StatusCodes.Status200OK);
                _logger.LogInformation("Done DeleteVideoGame {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<VideoGameDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Canceled DeleteVideoGame {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<VideoGameDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error DeleteVideoGame {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
