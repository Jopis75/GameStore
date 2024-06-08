﻿using Application.Dtos.Common;
using Application.Features.Companies.Requests.Queries;
using Application.Interfaces.Persistance;
using Domain.Dtos;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Companies.RequestHandlers.Queries
{
    public class ReadCompanyByIdRequestHandler : IRequestHandler<ReadCompanyByIdRequest, HttpResponseDto<CompanyDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IValidator<CompanyDto> _validator;

        private readonly ILogger<ReadCompanyByIdRequestHandler> _logger;

        public ReadCompanyByIdRequestHandler(IUnitOfWork unitOfWork, IValidator<CompanyDto> validator, ILogger<ReadCompanyByIdRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<CompanyDto>> Handle(ReadCompanyByIdRequest readCompanyByIdRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin ReadCompanyById {@ReadCompanyByIdRequest}.", readCompanyByIdRequest);

                cancellationToken.ThrowIfCancellationRequested();

                if (readCompanyByIdRequest.Id == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<CompanyDto>(new ArgumentNullException(nameof(readCompanyByIdRequest.Id)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error ReadCompanyById {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(readCompanyByIdRequest.Id, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var httpResponseDto1 = new HttpResponseDto<CompanyDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error ReadCompanyById {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var companyDto = await _unitOfWork.CompanyRepository.ReadByIdAsync(readCompanyByIdRequest.Id ?? 0, true);

                var httpResponseDto = new HttpResponseDto<CompanyDto>(companyDto, StatusCodes.Status200OK);
                _logger.LogInformation("Done ReadCompanyById {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<CompanyDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Canceled ReadCompanyById {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<CompanyDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error ReadCompanyById {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
