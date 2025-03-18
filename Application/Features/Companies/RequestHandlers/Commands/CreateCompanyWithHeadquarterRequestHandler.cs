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
    public class CreateCompanyWithHeadquarterRequestHandler : IRequestHandler<CreateCompanyWithHeadquarterRequest, HttpResponseDto<CompanyDto>>
    {
        private readonly ICompanyRepository _companyRepository;

        private readonly IAddressRepository _addressRepository;

        private readonly IMapper _mapper;

        private readonly IValidator<CreateCompanyWithHeadquarterRequest> _validator;

        private readonly ILogger<CreateCompanyWithHeadquarterRequestHandler> _logger;

        public CreateCompanyWithHeadquarterRequestHandler(ICompanyRepository companyRepository, IAddressRepository addressRepository, IMapper mapper, IValidator<CreateCompanyWithHeadquarterRequest> validator, ILogger<CreateCompanyWithHeadquarterRequestHandler> logger)
        {
            _companyRepository = companyRepository;
            _addressRepository = addressRepository;
            _mapper = mapper;
            _validator = validator;
            _logger = logger;
        }

        public async Task<HttpResponseDto<CompanyDto>> Handle(CreateCompanyWithHeadquarterRequest createCompanyWithHeadquarterRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin CreateCompanyWithHeadquarter {@CreateCompanyWithHeadquarterRequest}.", createCompanyWithHeadquarterRequest);

                if (createCompanyWithHeadquarterRequest == null)
                {
                    var ex = new ArgumentNullException(nameof(createCompanyWithHeadquarterRequest));
                    var httpResponseDto1 = new HttpResponseDto<CompanyDto>(ex.Message, StatusCodes.Status400BadRequest);
                    _logger.LogError(ex, "Error CreateCompanyWithHeadquarter {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(createCompanyWithHeadquarterRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var ex = new ValidationException(validationResult.Errors);
                    var httpResponseDto1 = new HttpResponseDto<CompanyDto>(ex.Message, StatusCodes.Status400BadRequest);
                    _logger.LogError(ex, "Error CreateCompanyWithHeadquarter {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var addressDto = _mapper.Map<AddressDto>(createCompanyWithHeadquarterRequest);
                var createdAddressDto = await _addressRepository.CreateAsync(addressDto, cancellationToken);
                createCompanyWithHeadquarterRequest.HeadquarterId = createdAddressDto.Id;
                var companyDto = _mapper.Map<CompanyDto>(createCompanyWithHeadquarterRequest);
                var createdCompanyDto = await _companyRepository.CreateAsync(companyDto, cancellationToken);

                var httpResponseDto = new HttpResponseDto<CompanyDto>(createdCompanyDto, StatusCodes.Status201Created);
                _logger.LogInformation("Done CreateCompanyWithHeadquarter {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<CompanyDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError(ex, "Canceled CreateCompanyWithHeadquarter {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<CompanyDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError(ex, "Error CreateCompanyWithHeadquarter {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
