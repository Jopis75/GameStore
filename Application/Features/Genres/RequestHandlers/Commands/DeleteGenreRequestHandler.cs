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
        private readonly IUnitOfWork _unitOfWork;

        private readonly IValidator<DeleteGenreRequest> _validator;

        private readonly ILogger<DeleteGenreRequestHandler> _logger;

        public DeleteGenreRequestHandler(IUnitOfWork unitOfWork, IValidator<DeleteGenreRequest> validator, ILogger<DeleteGenreRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<GenreDto>> Handle(DeleteGenreRequest deleteGenreRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin DeleteGenre {@DeleteGenreRequest}.", deleteGenreRequest);

                cancellationToken.ThrowIfCancellationRequested();

                if (deleteGenreRequest == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<GenreDto>(new ArgumentNullException(nameof(deleteGenreRequest)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error DeleteGenre {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(deleteGenreRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var httpResponseDto1 = new HttpResponseDto<GenreDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error DeleteGenre {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var deletedGenreDto = await _unitOfWork.GenreRepository.DeleteByIdAsync(deleteGenreRequest.Id, cancellationToken);
                await _unitOfWork.SaveAsync();

                var httpResponseDto = new HttpResponseDto<GenreDto>(deletedGenreDto, StatusCodes.Status200OK);
                _logger.LogInformation("Done DeleteGenre {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<GenreDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Canceled DeleteGenre {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<GenreDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error DeleteGenre {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
