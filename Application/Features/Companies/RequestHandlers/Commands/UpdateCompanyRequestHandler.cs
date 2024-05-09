using Application.Dtos.Common;
using Application.Dtos.Companies;
using Application.Features.Companies.Requests.Commands;
using Application.Interfaces.Persistance;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Companies.RequestHandlers.Commands
{
    public class UpdateCompanyRequestHandler : IRequestHandler<UpdateCompanyRequest, HttpResponseDto<UpdateCompanyResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IValidator<UpdateCompanyRequestDto> _validator;

        private readonly ILogger<UpdateCompanyRequestHandler> _logger;

        public UpdateCompanyRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<UpdateCompanyRequestDto> validator, ILogger<UpdateCompanyRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<UpdateCompanyResponseDto>> Handle(UpdateCompanyRequest updateCompanyRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin UpdateCompany {@UpdateCompanyRequest}.", updateCompanyRequest);

                cancellationToken.ThrowIfCancellationRequested();

                if (updateCompanyRequest.UpdateCompanyRequestDto == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<UpdateCompanyResponseDto>(new ArgumentNullException(nameof(updateCompanyRequest.UpdateCompanyRequestDto)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error UpdateCompany {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(updateCompanyRequest.UpdateCompanyRequestDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var httpResponseDto1 = new HttpResponseDto<UpdateCompanyResponseDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error UpdateCompany {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var company = await _unitOfWork.CompanyRepository.ReadByIdAsync(updateCompanyRequest.UpdateCompanyRequestDto.Id);
                _mapper.Map(updateCompanyRequest.UpdateCompanyRequestDto, company);
                var updatedCompany = await _unitOfWork.CompanyRepository.UpdateAsync(company);
                await _unitOfWork.SaveAsync();

                var httpResponseDto = new HttpResponseDto<UpdateCompanyResponseDto>(new UpdateCompanyResponseDto
                {
                    Id = updatedCompany.Id,
                    UpdatedAt = updatedCompany.UpdatedAt,
                    UpdatedBy = updatedCompany.UpdatedBy,
                }, StatusCodes.Status200OK);
                _logger.LogInformation("Done UpdateCompany {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<UpdateCompanyResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Canceled UpdateCompany {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<UpdateCompanyResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error UpdateCompany {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
