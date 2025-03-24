using Application.Dtos.General;
using Application.Features.Genres.Requests.Queries;
using Application.Interfaces.Persistance;
using Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Genres.RequestHandlers.Queries
{
    public class ReadGenreAllRequestHandler : IRequestHandler<ReadGenreAllRequest, HttpResponseDto<GenreDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly ILogger<ReadGenreAllRequestHandler> _logger;

        public ReadGenreAllRequestHandler(IUnitOfWork unitOfWork, ILogger<ReadGenreAllRequestHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<HttpResponseDto<GenreDto>> Handle(ReadGenreAllRequest readGenreAllRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin ReadGenreAll {@ReadGenreAllRequest}.", readGenreAllRequest);

                if (readGenreAllRequest == null)
                {
                    var ex = new ArgumentNullException(nameof(readGenreAllRequest));
                    var httpResponseDto1 = new HttpResponseDto<GenreDto>(ex.Message, StatusCodes.Status400BadRequest);
                    _logger.LogError(ex, "Error ReadGenreAll {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var genreDtos = await _unitOfWork.GenreRepository.ReadAllAsync(cancellationToken);

                var httpResponseDto = new HttpResponseDto<GenreDto>(genreDtos.ToArray(), StatusCodes.Status200OK);
                _logger.LogInformation("Done ReadGenreAll {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<GenreDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError(ex, "Canceled ReadGenreAll {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<GenreDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError(ex, "Error ReadGenreAll {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
