using Application.Dtos.General;
using Application.Features.Companies.Requests.Commands;
using Application.Interfaces.Persistance;
using Domain.Dtos;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Companies.RequestHandlers.Commands
{
    public class DeleteCompanyRequestHandler : IRequestHandler<DeleteCompanyRequest, HttpResponseDto<CompanyDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IValidator<DeleteCompanyRequest> _validator;

        private readonly ILogger<DeleteCompanyRequestHandler> _logger;

        public DeleteCompanyRequestHandler(IUnitOfWork unitOfWork, IValidator<DeleteCompanyRequest> validator, ILogger<DeleteCompanyRequestHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
            _logger = logger;
        }

        public async Task<HttpResponseDto<CompanyDto>> Handle(DeleteCompanyRequest deleteCompanyRequest, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                _logger.LogInformation("Begin DeleteCompany {@DeleteCompanyRequest}.", deleteCompanyRequest);

                if (deleteCompanyRequest == null)
                {
                    var ex = new ArgumentNullException(nameof(deleteCompanyRequest));
                    var httpResponseDto1 = new HttpResponseDto<CompanyDto>(ex.Message, StatusCodes.Status400BadRequest);
                    _logger.LogError(ex, "Error DeleteCompany {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(deleteCompanyRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var ex = new ValidationException(validationResult.Errors);
                    var httpResponseDto1 = new HttpResponseDto<CompanyDto>(ex.Message, StatusCodes.Status400BadRequest);
                    _logger.LogError(ex, "Error DeleteCompany {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var deletedCompanyDto = await _unitOfWork.CompanyRepository.DeleteByIdAsync(deleteCompanyRequest.Id, cancellationToken);

                await _unitOfWork.CommitTransactionAsync(cancellationToken);

                var httpResponseDto = new HttpResponseDto<CompanyDto>(deletedCompanyDto, StatusCodes.Status200OK);
                _logger.LogInformation("Done DeleteCompany {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);

                var httpResponseDto1 = new HttpResponseDto<CompanyDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError(ex, "Canceled DeleteCompany {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);

                var httpResponseDto1 = new HttpResponseDto<CompanyDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError(ex, "Error DeleteCompany {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
