using Application.Dtos.Common;
using Application.Dtos.Companies;
using Application.Features.Companies.Requests.Queries;
using Application.Interfaces.Persistance;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Companies.RequestHandlers.Queries
{
    public class ReadByIdCompanyRequestHandler : IRequestHandler<ReadByIdCompanyRequest, HttpResponseDto<ReadByIdCompanyResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IValidator<ReadByIdCompanyRequestDto> _validator;

        private readonly ILogger<ReadByIdCompanyRequestHandler> _logger;

        public ReadByIdCompanyRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<ReadByIdCompanyRequestDto> validator, ILogger<ReadByIdCompanyRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<ReadByIdCompanyResponseDto>> Handle(ReadByIdCompanyRequest readByIdCompanyRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin ReadByIdCompany {@ReadByIdCompanyRequest}.", readByIdCompanyRequest);

                if (readByIdCompanyRequest.ReadByIdCompanyRequestDto == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<ReadByIdCompanyResponseDto>(new ArgumentNullException(nameof(readByIdCompanyRequest.ReadByIdCompanyRequestDto)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error ReadByIdCompany {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(readByIdCompanyRequest.ReadByIdCompanyRequestDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var httpResponseDto1 = new HttpResponseDto<ReadByIdCompanyResponseDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error ReadByIdCompany {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var company = await _unitOfWork.CompanyRepository.ReadByIdAsync(readByIdCompanyRequest.ReadByIdCompanyRequestDto.Id, true);
                var readByIdCompanyResponseDto = _mapper.Map<ReadByIdCompanyResponseDto>(company);

                var httpResponseDto = new HttpResponseDto<ReadByIdCompanyResponseDto>(readByIdCompanyResponseDto, StatusCodes.Status200OK);
                _logger.LogInformation("End ReadByIdCompany {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ReadByIdCompanyResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error ReadByIdCompany {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
