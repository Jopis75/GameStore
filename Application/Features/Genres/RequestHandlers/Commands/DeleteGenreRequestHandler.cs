using Application.Dtos.General;
using Application.Features.Genres.Requests.Commands;
using Application.Interfaces.Persistance;
using Domain.Dtos;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Genres.RequestHandlers.Commands
{
    public class DeleteGenreRequestHandler : IRequestHandler<DeleteGenreRequest, HttpResponseDto<GenreDto>>
    {
        private readonly IGenreRepository _genreRepository;

        private readonly IValidator<DeleteGenreRequest> _validator;

        private readonly ILogger<DeleteGenreRequestHandler> _logger;

        public DeleteGenreRequestHandler(IGenreRepository genreRepository, IValidator<DeleteGenreRequest> validator, ILogger<DeleteGenreRequestHandler> logger)
        {
            _genreRepository = genreRepository;
            _validator = validator;
            _logger = logger;
        }

        public async Task<HttpResponseDto<GenreDto>> Handle(DeleteGenreRequest deleteGenreRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin DeleteGenre {@DeleteGenreRequest}.", deleteGenreRequest);

                if (deleteGenreRequest == null)
                {
                    var ex = new ArgumentNullException(nameof(deleteGenreRequest));
                    var httpResponseDto1 = new HttpResponseDto<GenreDto>(ex.Message, StatusCodes.Status400BadRequest);
                    _logger.LogError(ex, "Error DeleteGenre {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(deleteGenreRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var ex = new ValidationException(validationResult.Errors);
                    var httpResponseDto1 = new HttpResponseDto<GenreDto>(ex.Message, StatusCodes.Status400BadRequest);
                    _logger.LogError(ex, "Error DeleteGenre {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var deletedGenreDto = await _genreRepository.DeleteByIdAsync(deleteGenreRequest.Id, cancellationToken);

                var httpResponseDto = new HttpResponseDto<GenreDto>(deletedGenreDto, StatusCodes.Status200OK);
                _logger.LogInformation("Done DeleteGenre {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<GenreDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError(ex, "Canceled DeleteGenre {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<GenreDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError(ex, "Error DeleteGenre {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
