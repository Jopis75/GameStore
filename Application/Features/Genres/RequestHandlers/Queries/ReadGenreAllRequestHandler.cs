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
    public class ReadGenreAllRequestHandler : IRequestHandler<ReadGenreAllRequest, HttpResponseDto<GenreDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IValidator<ReadGenreAllRequest> _validator;

        private readonly ILogger<ReadGenreAllRequestHandler> _logger;

        public ReadGenreAllRequestHandler(IUnitOfWork unitOfWork, IValidator<ReadGenreAllRequest> validator, ILogger<ReadGenreAllRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<GenreDto>> Handle(ReadGenreAllRequest readGenreAllRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin ReadGenreAll {@ReadGenreAllRequest}.", readGenreAllRequest);

                cancellationToken.ThrowIfCancellationRequested();

                var genreDtos = await _unitOfWork.GenreRepository.ReadAllAsync(cancellationToken);

                var httpResponseDto = new HttpResponseDto<GenreDto>(genreDtos.ToArray(), StatusCodes.Status200OK);
                _logger.LogInformation("Done ReadGenreAll {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<GenreDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Canceled ReadGenreAll {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<GenreDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error ReadGenreAll {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
