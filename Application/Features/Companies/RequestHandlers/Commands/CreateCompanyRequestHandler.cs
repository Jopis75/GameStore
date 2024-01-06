using Application.Dtos.Common;
using Application.Dtos.Companies;
using Application.Features.Companies.Requests.Commands;
using Application.Interfaces.Persistance;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Companies.RequestHandlers.Commands
{
    public class CreateCompanyRequestHandler : IRequestHandler<CreateCompanyRequest, HttpResponseDto<CreateCompanyResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IValidator<CreateCompanyRequestDto> _validator;

        private readonly ILogger<CreateCompanyRequestHandler> _logger;

        public CreateCompanyRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<CreateCompanyRequestDto> validator, ILogger<CreateCompanyRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<CreateCompanyResponseDto>> Handle(CreateCompanyRequest createCompanyRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin CreateCompany {@CreateCompanyRequest}.", createCompanyRequest);

                if (createCompanyRequest.CreateCompanyRequestDto == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<CreateCompanyResponseDto>(new ArgumentNullException(nameof(createCompanyRequest.CreateCompanyRequestDto)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error CreateCompany {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(createCompanyRequest.CreateCompanyRequestDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var httpResponseDto1 = new HttpResponseDto<CreateCompanyResponseDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error CreateCompany {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var company = _mapper.Map<Company>(createCompanyRequest.CreateCompanyRequestDto);
                var createdCompany = await _unitOfWork.CompanyRepository.CreateAsync(company);
                await _unitOfWork.SaveAsync();

                var httpResponseDto = new HttpResponseDto<CreateCompanyResponseDto>(new CreateCompanyResponseDto
                {
                    Id = createdCompany.Id,
                    CreatedAt = createdCompany.CreatedAt,
                    CreatedBy = createdCompany.CreatedBy,
                }, StatusCodes.Status201Created);
                _logger.LogInformation("Done CreateCompany {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<CreateCompanyResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error CreateCompany {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
