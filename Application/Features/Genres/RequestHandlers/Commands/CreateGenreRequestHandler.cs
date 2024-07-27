using Application.Dtos.General;
using Application.Features.Genres.Requests.Commands;
using Application.Interfaces.Persistance;
using Application.Validators.Requests.Genres.Commands;
using Domain.Dtos;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Genres.RequestHandlers.Commands
{
    public class CreateGenreRequestHandler : IRequestHandler<CreateGenreRequest, HttpResponseDto<GenreDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IValidator<CreateGenreRequest> _validator;

        private readonly ILogger<CreateGenreRequestHandler> _logger;

        public CreateGenreRequestHandler(IUnitOfWork unitOfWork, IValidator<CreateGenreRequest> validator, ILogger<CreateGenreRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<GenreDto>> Handle(CreateGenreRequest createGenreRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin CreateGenre {@CreateGenreRequest}.", createGenreRequest);

                cancellationToken.ThrowIfCancellationRequested();

                if (createGenreRequest == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<GenreDto>(new ArgumentNullException(nameof(createGenreRequest)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error CreateGenre {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(createGenreRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var httpResponseDto1 = new HttpResponseDto<GenreDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error CreateGenre {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var createdGenreDto = await _unitOfWork.GenreRepository.CreateAsync(createGenreRequest.GenreDto, cancellationToken);
                await _unitOfWork.SaveAsync();

                var httpResponseDto = new HttpResponseDto<GenreDto>(createdGenreDto, StatusCodes.Status201Created);
                _logger.LogInformation("Done CreateGenre {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<GenreDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Canceled CreateGenre {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<GenreDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error CreateGenre {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
