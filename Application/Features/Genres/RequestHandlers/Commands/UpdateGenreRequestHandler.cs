﻿using Application.Dtos.General;
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
    public class UpdateGenreRequestHandler : IRequestHandler<UpdateGenreRequest, HttpResponseDto<GenreDto>>
    {
        private readonly IGenreRepository _genreRepository;

        private readonly IMapper _mapper;

        private readonly IValidator<UpdateGenreRequest> _validator;

        private readonly ILogger<UpdateGenreRequestHandler> _logger;

        public UpdateGenreRequestHandler(IGenreRepository genreRepository, IMapper mapper, IValidator<UpdateGenreRequest> validator, ILogger<UpdateGenreRequestHandler> logger)
        {
            _genreRepository = genreRepository ?? throw new ArgumentNullException(nameof(genreRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<GenreDto>> Handle(UpdateGenreRequest updateGenreRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin UpdateGenre {@UpdateGenreRequest}.", updateGenreRequest);

                if (updateGenreRequest == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<GenreDto>(new ArgumentNullException(nameof(updateGenreRequest)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error UpdateGenre {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(updateGenreRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var httpResponseDto1 = new HttpResponseDto<GenreDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error UpdateGenre {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var genreDto = _mapper.Map<GenreDto>(updateGenreRequest);
                var updatedGenreDto = await _genreRepository.UpdateAsync(genreDto, cancellationToken);

                var httpResponseDto = new HttpResponseDto<GenreDto>(updatedGenreDto, StatusCodes.Status200OK);
                _logger.LogInformation("Done UpdateGenre {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<GenreDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Canceled UpdateGenre {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<GenreDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error UpdateGenre {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
