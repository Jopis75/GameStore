﻿using Application.Dtos.General;
using Application.Features.Companies.Requests.Commands;
using Application.Interfaces.Persistance;
using AutoMapper;
using Domain.Dtos;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Companies.RequestHandlers.Commands
{
    public class UpdateCompanyRequestHandler : IRequestHandler<UpdateCompanyRequest, HttpResponseDto<CompanyDto>>
    {
        private readonly ICompanyRepository _companyRepository;

        private readonly IMapper _mapper;

        private readonly IValidator<UpdateCompanyRequest> _validator;

        private readonly ILogger<UpdateCompanyRequestHandler> _logger;

        public UpdateCompanyRequestHandler(ICompanyRepository companyRepository, IMapper mapper, IValidator<UpdateCompanyRequest> validator, ILogger<UpdateCompanyRequestHandler> logger)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
            _validator = validator;
            _logger = logger;
        }

        public async Task<HttpResponseDto<CompanyDto>> Handle(UpdateCompanyRequest updateCompanyRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin UpdateCompany {@UpdateCompanyRequest}.", updateCompanyRequest);

                if (updateCompanyRequest == null)
                {
                    var ex = new ArgumentNullException(nameof(updateCompanyRequest));
                    var httpResponseDto1 = new HttpResponseDto<CompanyDto>(ex.Message, StatusCodes.Status400BadRequest);
                    _logger.LogError(ex, "Error UpdateCompany {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(updateCompanyRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var ex = new ValidationException(validationResult.Errors);
                    var httpResponseDto1 = new HttpResponseDto<CompanyDto>(ex.Message, StatusCodes.Status400BadRequest);
                    _logger.LogError(ex, "Error UpdateCompany {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var companyDto = _mapper.Map<CompanyDto>(updateCompanyRequest);
                var updatedCompanyDto = await _companyRepository.UpdateAsync(companyDto, cancellationToken);

                var httpResponseDto = new HttpResponseDto<CompanyDto>(updatedCompanyDto, StatusCodes.Status200OK);
                _logger.LogInformation("Done UpdateCompany {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<CompanyDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError(ex, "Canceled UpdateCompany {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<CompanyDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError(ex, "Error UpdateCompany {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
