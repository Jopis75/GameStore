using Application.Dtos.Common;
using Application.Dtos.Reviews;
using Application.Features.Reviews.Requests.Queries;
using Application.Interfaces.Persistance;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Reviews.RequestHandlers.Queries
{
    public class ReadAllReviewRequestHandler : IRequestHandler<ReadAllReviewRequest, HttpResponseDto<ReadAllReviewResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public ReadAllReviewRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<HttpResponseDto<ReadAllReviewResponseDto>> Handle(ReadAllReviewRequest readAllReviewRequest, CancellationToken cancellationToken)
        {
            try
            {
                var reviews = await _unitOfWork.ReviewRepository.ReadAllAsync(true);
                var readAllReviewResponseDtos = reviews
                    .Select(_mapper.Map<ReadAllReviewResponseDto>)
                    .ToList();

                return new HttpResponseDto<ReadAllReviewResponseDto>(readAllReviewResponseDtos, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return new HttpResponseDto<ReadAllReviewResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }
    }
}
