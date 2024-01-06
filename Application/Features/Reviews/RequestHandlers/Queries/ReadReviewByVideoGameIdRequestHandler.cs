using Application.Dtos.Common;
using Application.Dtos.Reviews;
using Application.Features.Reviews.Requests.Queries;
using Application.Interfaces.Persistance;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Reviews.RequestHandlers.Queries
{
    public class ReadReviewByVideoGameIdRequestHandler : IRequestHandler<ReadReviewByVideoGameIdRequest, HttpResponseDto<ReadReviewResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IValidator<ReadReviewByVideoGameIdRequestDto> _validator;

        private readonly ILogger<ReadReviewByVideoGameIdRequestHandler> _logger;

        public ReadReviewByVideoGameIdRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<ReadReviewByVideoGameIdRequestDto> validator, ILogger<ReadReviewByVideoGameIdRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<ReadReviewResponseDto>> Handle(ReadReviewByVideoGameIdRequest readReviewByVideoGameIdRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin ReadReviewByVideoGameId {@ReadReviewByVideoGameIdRequest}.", readReviewByVideoGameIdRequest);

                if (readReviewByVideoGameIdRequest.ReadReviewByVideoGameIdRequestDto == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<ReadReviewResponseDto>(new ArgumentNullException(nameof(readReviewByVideoGameIdRequest.ReadReviewByVideoGameIdRequestDto)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error ReadReviewByVideoGameId {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(readReviewByVideoGameIdRequest.ReadReviewByVideoGameIdRequestDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var httpResponseDto1 = new HttpResponseDto<ReadReviewResponseDto>(new ArgumentNullException(nameof(readReviewByVideoGameIdRequest.ReadReviewByVideoGameIdRequestDto)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error ReadReviewByVideoGameId {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var review = await _unitOfWork.ReviewRepository.ReadByVideoGameIdAsync(readReviewByVideoGameIdRequest.ReadReviewByVideoGameIdRequestDto.VideoGameId, true);
                var readReviewResponseDto = _mapper.Map<ReadReviewResponseDto>(review);

                var httpResponseDto = new HttpResponseDto<ReadReviewResponseDto>(readReviewResponseDto, StatusCodes.Status200OK);
                _logger.LogInformation("Done ReadReviewByVideoGameId {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ReadReviewResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error ReadReviewByVideoGameId {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
