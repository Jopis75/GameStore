﻿using Application.Dtos.General;
using Application.Features.Reviews.Requests.Commands;
using Application.Interfaces.Persistance;
using AutoMapper;
using Domain.Dtos;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Reviews.RequestHandlers.Commands
{
    public class CreateReviewRequestHandler : IRequestHandler<CreateReviewRequest, HttpResponseDto<ReviewDto>>
    {
        private readonly IReviewRepository _reviewRepository;

        private readonly IMapper _mapper;

        private readonly IValidator<CreateReviewRequest> _validator;

        private readonly ILogger<CreateReviewRequestHandler> _logger;

        public CreateReviewRequestHandler(IReviewRepository reviewRepository, IMapper mapper, IValidator<CreateReviewRequest> validator, ILogger<CreateReviewRequestHandler> logger)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
            _validator = validator;
            _logger = logger;
        }

        public async Task<HttpResponseDto<ReviewDto>> Handle(CreateReviewRequest createReviewRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin CreateReview {@CreateReviewRequest}.", createReviewRequest);

                if (createReviewRequest == null)
                {
                    var ex = new ArgumentNullException(nameof(createReviewRequest));
                    var httpResponseDto1 = new HttpResponseDto<ReviewDto>(ex.Message, StatusCodes.Status400BadRequest);
                    _logger.LogError(ex, "Error CreateReview {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(createReviewRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var ex = new ValidationException(validationResult.Errors);
                    var httpResponseDto1 = new HttpResponseDto<ReviewDto>(ex.Message, StatusCodes.Status400BadRequest);
                    _logger.LogError(ex, "Error CreateReview {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var reviewDto = _mapper.Map<ReviewDto>(createReviewRequest);
                var createdReviewDto = await _reviewRepository.CreateAsync(reviewDto, cancellationToken);

                var httpResponseDto = new HttpResponseDto<ReviewDto>(createdReviewDto, StatusCodes.Status201Created);
                _logger.LogInformation("Done CreateReview {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ReviewDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError(ex, "Canceled CreateReview {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ReviewDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError(ex, "Error CreateReview {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
