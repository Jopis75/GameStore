using Application.Dtos.Common;
using Application.Dtos.ConsoleVideoGames;
using Application.Dtos.Reviews;
using Application.Features.Reviews.Requests.Queries;
using Application.Interfaces.Persistance;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Reviews.RequestHandlers.Queries
{
    public class ReadReviewAllRequestHandler : IRequestHandler<ReadReviewAllRequest, HttpResponseDto<List<ReadReviewResponseDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly ILogger<ReadReviewAllRequestHandler> _logger;

        public ReadReviewAllRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ReadReviewAllRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<List<ReadReviewResponseDto>>> Handle(ReadReviewAllRequest readReviewAllRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin ReadReviewAll {@ReadReviewAllRequest}.", readReviewAllRequest);

                cancellationToken.ThrowIfCancellationRequested();

                var reviews = await _unitOfWork.ReviewRepository.ReadAllAsync(true);
                var readReviewResponseDtos = reviews
                    .Select(_mapper.Map<ReadReviewResponseDto>)
                    .ToList();

                var httpResponseDto = new HttpResponseDto<List<ReadReviewResponseDto>>(readReviewResponseDtos, StatusCodes.Status200OK);
                _logger.LogInformation("Done ReadReviewAll {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<List<ReadReviewResponseDto>>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Canceled ReadReviewAll {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<List<ReadReviewResponseDto>>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error ReadReviewAll {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
