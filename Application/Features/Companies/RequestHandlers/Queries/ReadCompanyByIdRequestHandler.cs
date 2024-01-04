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
    public class ReadCompanyByIdRequestHandler : IRequestHandler<ReadCompanyByIdRequest, HttpResponseDto<ReadCompanyResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IValidator<ReadByIdRequestDto> _validator;

        private readonly ILogger<ReadCompanyByIdRequestHandler> _logger;

        public ReadCompanyByIdRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<ReadByIdRequestDto> validator, ILogger<ReadCompanyByIdRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<ReadCompanyResponseDto>> Handle(ReadCompanyByIdRequest readByIdCompanyRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin ReadByIdCompany {@ReadByIdCompanyRequest}.", readByIdCompanyRequest);

                if (readByIdCompanyRequest.ReadByIdRequestDto == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<ReadCompanyResponseDto>(new ArgumentNullException(nameof(readByIdCompanyRequest.ReadByIdRequestDto)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error ReadByIdCompany {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(readByIdCompanyRequest.ReadByIdRequestDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var httpResponseDto1 = new HttpResponseDto<ReadCompanyResponseDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error ReadByIdCompany {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var company = await _unitOfWork.CompanyRepository.ReadByIdAsync(readByIdCompanyRequest.ReadByIdRequestDto.Id, true);
                var readByIdCompanyResponseDto = _mapper.Map<ReadCompanyResponseDto>(company);

                var httpResponseDto = new HttpResponseDto<ReadCompanyResponseDto>(readByIdCompanyResponseDto, StatusCodes.Status200OK);
                _logger.LogInformation("End ReadByIdCompany {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ReadCompanyResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error ReadByIdCompany {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
