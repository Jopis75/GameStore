using FluentValidation;
using GameStore.Application.Dtos.General;
using GameStore.Application.Exceptions;
using GameStore.Application.Features.Companies.Requests.Queries;
using GameStore.Application.Interfaces.Infrastructure;
using GameStore.Application.Interfaces.Persistance;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace GameStore.Application.Features.Companies.RequestHandlers.Queries
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

                if (!validationResult.IsValid)
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
