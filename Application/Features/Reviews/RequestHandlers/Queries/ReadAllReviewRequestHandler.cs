using Application.Dtos.Common;
using Application.Dtos.Reviews;
using Application.Features.Reviews.Requests.Queries;
using Application.Interfaces.Persistance;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Reviews.RequestHandlers.Queries
{
    public class ReadAllReviewRequestHandler : IRequestHandler<ReadAllReviewRequest, HttpResponseDto<ReadAllReviewResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly ILogger<ReadAllReviewRequestHandler> _logger;

        public ReadAllReviewRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ReadAllReviewRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<ReadAllReviewResponseDto>> Handle(ReadAllReviewRequest readAllReviewRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin ReadAllReview {@ReadAllReviewRequest}.", readAllReviewRequest);

                var reviews = await _unitOfWork.ReviewRepository.ReadAllAsync(true);
                var readAllReviewResponseDtos = reviews
                    .Select(_mapper.Map<ReadAllReviewResponseDto>)
                    .ToList();

                var httpResponseDto = new HttpResponseDto<ReadAllReviewResponseDto>(readAllReviewResponseDtos, StatusCodes.Status200OK);
                _logger.LogInformation("End ReadAllReview {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ReadAllReviewResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error ReadAllReview {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
