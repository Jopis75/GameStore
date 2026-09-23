using Application.Dtos.General;
using Application.Exceptions;
using Application.Features.Addresses.Requests.Queries;
using Application.Interfaces.Infrastructure;
using Application.Interfaces.Persistance;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Addresses.RequestHandlers.Queries
{
    public class ReadAddressByIdRequestHandler(IUnitOfWork unitOfWork, IValidator<ReadAddressByIdRequest> validator, ILoggerService<ReadAddressByIdRequestHandler> loggerService) : IRequestHandler<ReadAddressByIdRequest, HttpResponseDto<ReadAddressByIdRequest>>
    {
        public async Task<HttpResponseDto<ReadAddressByIdRequest>> Handle(ReadAddressByIdRequest readAddressByIdRequest, CancellationToken cancellationToken)
        {
            var methodName = "HandleReadAddressByIdRequest";

            try
            {
                loggerService.LogBeginInformation(methodName, readAddressByIdRequest);

                if (readAddressByIdRequest == null)
                {
                    return loggerService.LogArgumentNullException<ReadAddressByIdRequest>(new ArgumentNullException(nameof(readAddressByIdRequest)), methodName);
                }

                var validationResult = await validator.ValidateAsync(readAddressByIdRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    return loggerService.LogValidationException<ReadAddressByIdRequest>(new ValidationException(validationResult.Errors), methodName);
                }

                var addressDto = await unitOfWork.AddressRepository.ReadByIdAsync(readAddressByIdRequest.Id, cancellationToken);

                if (addressDto.IsNullObject)
                {
                    return loggerService.LogNotFoundException<ReadAddressByIdRequest>(new NotFoundException($"Could not find Address object with Id {readAddressByIdRequest.Id}."), methodName);
                }

                return loggerService.LogDoneInformation<ReadAddressByIdRequest>(methodName, readAddressByIdRequest, StatusCodes.Status200OK);
            }
            catch (OperationCanceledException ex)
            {
                return loggerService.LogOperationCanceledException<ReadAddressByIdRequest>(ex, methodName);
            }
            catch (Exception ex)
            {
                return loggerService.LogException<ReadAddressByIdRequest>(ex, methodName);
            }
        }
    }
}
