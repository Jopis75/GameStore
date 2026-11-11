using FluentValidation;
using GameStore.Application.Dtos.General;
using GameStore.Application.Exceptions;
using GameStore.Application.Features.Addresses.Requests.Queries;
using GameStore.Application.Interfaces.Infrastructure;
using GameStore.Application.Interfaces.Persistance;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace GameStore.Application.Features.Addresses.RequestHandlers.Queries
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

                if (!validationResult.IsValid)
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
