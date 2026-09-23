using Application.Dtos.General;
using Application.Exceptions;
using Application.Features.Companies.Requests.Queries;
using Application.Interfaces.Infrastructure;
using Application.Interfaces.Persistance;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Companies.RequestHandlers.Queries
{
    public class ReadCompanyByIdRequestHandler(IUnitOfWork unitOfWork, IValidator<ReadCompanyByIdRequest> validator, ILoggerService<ReadCompanyByIdRequestHandler> loggerService) : IRequestHandler<ReadCompanyByIdRequest, HttpResponseDto<ReadCompanyByIdRequest>>
    {
        public async Task<HttpResponseDto<ReadCompanyByIdRequest>> Handle(ReadCompanyByIdRequest readCompanyByIdRequest, CancellationToken cancellationToken)
        {
            var methodName = "HandleReadCompanyByIdRequest";

            try
            {
                loggerService.LogBeginInformation(methodName, readCompanyByIdRequest);

                if (readCompanyByIdRequest == null)
                {
                    return loggerService.LogArgumentNullException<ReadCompanyByIdRequest>(new ArgumentNullException(nameof(readCompanyByIdRequest)), methodName);
                }

                var validationResult = await validator.ValidateAsync(readCompanyByIdRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    return loggerService.LogValidationException<ReadCompanyByIdRequest>(new ValidationException(validationResult.Errors), methodName);
                }

                var companyDto = await unitOfWork.CompanyRepository.ReadByIdAsync(readCompanyByIdRequest.Id, cancellationToken);

                if (companyDto.IsNullObject)
                {
                    return loggerService.LogNotFoundException<ReadCompanyByIdRequest>(new NotFoundException($"Could not find Company object with Id {readCompanyByIdRequest.Id}."), methodName);
                }

                return loggerService.LogDoneInformation(methodName, readCompanyByIdRequest, StatusCodes.Status200OK);
            }
            catch (OperationCanceledException ex)
            {
                return loggerService.LogOperationCanceledException<ReadCompanyByIdRequest>(ex, methodName);
            }
            catch (Exception ex)
            {
                return loggerService.LogException<ReadCompanyByIdRequest>(ex, methodName);
            }
        }
    }
}
