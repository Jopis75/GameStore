using Application.Dtos.General;
using Application.Features.Genres.Requests.Queries;
using Application.Interfaces.Persistance;
using Domain.Dtos;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Genres.RequestHandlers.Queries
{
    public class ReadGenreByIdRequestHandler : IRequestHandler<ReadGenreByIdRequest, HttpResponseDto<GenreDto>>
    {
        private readonly IGenreRepository _genreRepository;

        private readonly IValidator<ReadGenreByIdRequest> _validator;

        private readonly ILogger<ReadGenreByIdRequestHandler> _logger;

        public ReadGenreByIdRequestHandler(IGenreRepository genreRepository, IValidator<ReadGenreByIdRequest> validator, ILogger<ReadGenreByIdRequestHandler> logger)
        {
            _genreRepository = genreRepository ?? throw new ArgumentNullException(nameof(genreRepository));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<GenreDto>> Handle(ReadGenreByIdRequest readGenreByIdRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin ReadGenreById {@ReadGenreByIdRequest}.", readGenreByIdRequest);

                if (readGenreByIdRequest == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<GenreDto>(new ArgumentNullException(nameof(readGenreByIdRequest)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error ReadGenreById {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(readGenreByIdRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var httpResponseDto1 = new HttpResponseDto<GenreDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error ReadGenreById {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var genreDto = await _genreRepository.ReadByIdAsync(readGenreByIdRequest.Id, cancellationToken);

                var httpResponseDto = new HttpResponseDto<GenreDto>(genreDto, StatusCodes.Status200OK);
                _logger.LogInformation("Done ReadGenreById {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<GenreDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Canceled ReadGenreById {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<GenreDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error ReadGenreById {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
