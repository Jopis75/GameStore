using Application.Dtos.General;
using Application.Features.VideoGames.Requests.Commands;
using Application.Interfaces.Persistance;
using AutoMapper;
using Domain.Dtos;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.VideoGames.RequestHandlers.Commands
{
    public class DeleteVideoGameRequestHandler : IRequestHandler<DeleteVideoGameRequest, HttpResponseDto<VideoGameDto>>
    {
        private readonly IVideoGameRepository _videoGameRepository;

        private readonly IValidator<DeleteVideoGameRequest> _validator;

        private readonly ILogger<DeleteVideoGameRequestHandler> _logger;

        public DeleteVideoGameRequestHandler(IVideoGameRepository videoGameRepository, IValidator<DeleteVideoGameRequest> validator, ILogger<DeleteVideoGameRequestHandler> logger)
        {
            _videoGameRepository = videoGameRepository;
            _validator = validator;
            _logger = logger;
        }

        public async Task<HttpResponseDto<VideoGameDto>> Handle(DeleteVideoGameRequest deleteVideoGameRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin DeleteVideoGame {@DeleteVideoGameRequest}.", deleteVideoGameRequest);

                if (deleteVideoGameRequest == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<VideoGameDto>(new ArgumentNullException(nameof(deleteVideoGameRequest)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error DeleteVideoGame {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(deleteVideoGameRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var httpResponseDto1 = new HttpResponseDto<VideoGameDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error DeleteVideoGame {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var deletedVideoGameDto = await _videoGameRepository.DeleteByIdAsync(deleteVideoGameRequest.Id, cancellationToken);

                var httpResponseDto = new HttpResponseDto<VideoGameDto>(deletedVideoGameDto, StatusCodes.Status200OK);
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
