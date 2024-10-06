using Application.Dtos.General;
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
        private readonly ICompanyRepository _companyRepository;

        private readonly IValidator<ReadCompanyByIdRequest> _validator;

        private readonly ILogger<ReadCompanyByIdRequestHandler> _logger;

        public ReadCompanyByIdRequestHandler(ICompanyRepository companyRepository, IValidator<ReadCompanyByIdRequest> validator, ILogger<ReadCompanyByIdRequestHandler> logger)
        {
            _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<CompanyDto>> Handle(ReadCompanyByIdRequest readCompanyByIdRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin ReadCompanyById {@ReadCompanyByIdRequest}.", readCompanyByIdRequest);

                if (readCompanyByIdRequest == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<CompanyDto>(new ArgumentNullException(nameof(readCompanyByIdRequest)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error ReadCompanyById {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(readCompanyByIdRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var httpResponseDto1 = new HttpResponseDto<CompanyDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error ReadCompanyById {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var companyDto = await _companyRepository.ReadByIdAsync(readCompanyByIdRequest.Id, cancellationToken);

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
