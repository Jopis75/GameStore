using Application.Dtos.General;
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
    public class CreateCompanyRequestHandler : IRequestHandler<CreateCompanyRequest, HttpResponseDto<CompanyDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IValidator<CreateCompanyRequest> _validator;

        private readonly ILogger<CreateCompanyRequestHandler> _logger;

        public CreateCompanyRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<CreateCompanyRequest> validator, ILogger<CreateCompanyRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<CompanyDto>> Handle(CreateCompanyRequest createCompanyRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin CreateCompany {@CreateCompanyRequest}.", createCompanyRequest);

                if (createCompanyRequest == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<CompanyDto>(new ArgumentNullException(nameof(createCompanyRequest)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error CreateCompany {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(createCompanyRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var httpResponseDto1 = new HttpResponseDto<CompanyDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error CreateCompany {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var companyDto = _mapper.Map<CompanyDto>(createCompanyRequest);
                var createdCompanyDto = await _unitOfWork.CompanyRepository.CreateAsync(companyDto, cancellationToken);
                
                await _unitOfWork.SaveAsync();

                var httpResponseDto = new HttpResponseDto<CompanyDto>(createdCompanyDto, StatusCodes.Status201Created);
                _logger.LogInformation("Done CreateCompany {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<CompanyDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Canceled CreateCompany {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<CompanyDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error CreateCompany {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
