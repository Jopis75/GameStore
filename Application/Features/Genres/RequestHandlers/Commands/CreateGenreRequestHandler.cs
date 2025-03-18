using Application.Dtos.General;
using Application.Features.Genres.Requests.Commands;
using Application.Interfaces.Persistance;
using AutoMapper;
using Domain.Dtos;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Genres.RequestHandlers.Commands
{
    public class CreateGenreRequestHandler : IRequestHandler<CreateGenreRequest, HttpResponseDto<GenreDto>>
    {
        private readonly IGenreRepository _genreRepository;

        private readonly IMapper _mapper;

        private readonly IValidator<CreateGenreRequest> _validator;

        private readonly ILogger<CreateGenreRequestHandler> _logger;

        public CreateGenreRequestHandler(IGenreRepository genreRepository, IMapper mapper, IValidator<CreateGenreRequest> validator, ILogger<CreateGenreRequestHandler> logger)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
            _validator = validator;
            _logger = logger;
        }

        public async Task<HttpResponseDto<GenreDto>> Handle(CreateGenreRequest createGenreRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin CreateGenre {@CreateGenreRequest}.", createGenreRequest);

                if (createGenreRequest == null)
                {
                    var ex = new ArgumentNullException(nameof(createGenreRequest));
                    var httpResponseDto1 = new HttpResponseDto<GenreDto>(ex.Message, StatusCodes.Status400BadRequest);
                    _logger.LogError(ex, "Error CreateGenre {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(createGenreRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var ex = new ValidationException(validationResult.Errors);
                    var httpResponseDto1 = new HttpResponseDto<GenreDto>(ex.Message, StatusCodes.Status400BadRequest);
                    _logger.LogError(ex, "Error CreateGenre {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var genreDto = _mapper.Map<GenreDto>(createGenreRequest);
                var createdGenreDto = await _genreRepository.CreateAsync(genreDto, cancellationToken);

                var httpResponseDto = new HttpResponseDto<GenreDto>(createdGenreDto, StatusCodes.Status201Created);
                _logger.LogInformation("Done CreateGenre {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<GenreDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError(ex, "Canceled CreateGenre {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<GenreDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError(ex, "Error CreateGenre {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
