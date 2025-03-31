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
    public class DeleteGenreRequestHandler(IUnitOfWork unitOfWork, IValidator<DeleteGenreRequest> validator, ILogger<DeleteGenreRequestHandler> logger) : IRequestHandler<DeleteGenreRequest, HttpResponseDto<GenreDto>>
    {
        public async Task<HttpResponseDto<GenreDto>> Handle(DeleteGenreRequest deleteGenreRequest, CancellationToken cancellationToken)
        {
            await unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                logger.LogInformation("Begin DeleteGenre {@DeleteGenreRequest}.", deleteGenreRequest);

                if (deleteGenreRequest == null)
                {
                    var ex = new ArgumentNullException(nameof(deleteGenreRequest));
                    var httpResponseDto1 = new HttpResponseDto<GenreDto>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error DeleteGenre {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await validator.ValidateAsync(deleteGenreRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var ex = new ValidationException(validationResult.Errors);
                    var httpResponseDto1 = new HttpResponseDto<GenreDto>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error DeleteGenre {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var deletedGenreDto = await unitOfWork.GenreRepository.DeleteByIdAsync(deleteGenreRequest.Id, cancellationToken);

                await unitOfWork.CommitTransactionAsync(cancellationToken);

                var httpResponseDto = new HttpResponseDto<GenreDto>(deletedGenreDto, StatusCodes.Status200OK);
                logger.LogInformation("Done DeleteGenre {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                await unitOfWork.RollbackTransactionAsync(cancellationToken);

                var httpResponseDto1 = new HttpResponseDto<GenreDto>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Canceled DeleteGenre {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackTransactionAsync(cancellationToken);

                var httpResponseDto1 = new HttpResponseDto<GenreDto>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Error DeleteGenre {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
