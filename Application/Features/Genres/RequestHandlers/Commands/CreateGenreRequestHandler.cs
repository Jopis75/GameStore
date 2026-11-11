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
    public class CreateGenreRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<CreateGenreRequest> validator, ILogger<CreateGenreRequestHandler> logger) : IRequestHandler<CreateGenreRequest, HttpResponseDto<GenreDto>>
    {
        public async Task<HttpResponseDto<GenreDto>> Handle(CreateGenreRequest createGenreRequest, CancellationToken cancellationToken)
        {
            await unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                logger.LogInformation("Begin CreateGenre {@CreateGenreRequest}.", createGenreRequest);

                if (createGenreRequest == null)
                {
                    var ex = new ArgumentNullException(nameof(createGenreRequest));
                    var httpResponseDto1 = new HttpResponseDto<GenreDto>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error CreateGenre {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await validator.ValidateAsync(createGenreRequest, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var ex = new ValidationException(validationResult.Errors);
                    var httpResponseDto1 = new HttpResponseDto<GenreDto>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error CreateGenre {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var genreDto = mapper.Map<GenreDto>(createGenreRequest);
                var createdGenreDto = await unitOfWork.GenreRepository.CreateAsync(genreDto, cancellationToken);

                await unitOfWork.CommitTransactionAsync(cancellationToken);

                var httpResponseDto = new HttpResponseDto<GenreDto>(createdGenreDto, StatusCodes.Status201Created);
                logger.LogInformation("Done CreateGenre {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                await unitOfWork.RollbackTransactionAsync(cancellationToken);

                var httpResponseDto1 = new HttpResponseDto<GenreDto>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Canceled CreateGenre {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackTransactionAsync(cancellationToken);

                var httpResponseDto1 = new HttpResponseDto<GenreDto>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Error CreateGenre {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
