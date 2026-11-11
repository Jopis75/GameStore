using AutoMapper;
using FluentValidation;
using GameStore.Application.Dtos.General;
using GameStore.Application.Features.Genres.Requests.Commands;
using GameStore.Application.Interfaces.Persistance;
using GameStore.Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace GameStore.Application.Features.Genres.RequestHandlers.Commands
{
    public class UpdateGenreRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<UpdateGenreRequest> validator, ILogger<UpdateGenreRequestHandler> logger) : IRequestHandler<UpdateGenreRequest, HttpResponseDto<GenreDto>>
    {
        public async Task<HttpResponseDto<GenreDto>> Handle(UpdateGenreRequest updateGenreRequest, CancellationToken cancellationToken)
        {
            await unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                logger.LogInformation("Begin UpdateGenre {@UpdateGenreRequest}.", updateGenreRequest);

                if (updateGenreRequest == null)
                {
                    var ex = new ArgumentNullException(nameof(updateGenreRequest));
                    var httpResponseDto1 = new HttpResponseDto<GenreDto>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error UpdateGenre {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await validator.ValidateAsync(updateGenreRequest, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var ex = new ValidationException(validationResult.Errors);
                    var httpResponseDto1 = new HttpResponseDto<GenreDto>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error UpdateGenre {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var genreDto = mapper.Map<GenreDto>(updateGenreRequest);
                var updatedGenreDto = await unitOfWork.GenreRepository.UpdateAsync(genreDto, cancellationToken);

                await unitOfWork.CommitTransactionAsync(cancellationToken);

                var httpResponseDto = new HttpResponseDto<GenreDto>(updatedGenreDto, StatusCodes.Status200OK);
                logger.LogInformation("Done UpdateGenre {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                await unitOfWork.RollbackTransactionAsync(cancellationToken);

                var httpResponseDto1 = new HttpResponseDto<GenreDto>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Canceled UpdateGenre {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackTransactionAsync(cancellationToken);

                var httpResponseDto1 = new HttpResponseDto<GenreDto>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Error UpdateGenre {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
