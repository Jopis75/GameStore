using GameStore.Application.Dtos.General;
using GameStore.Application.Features.Addresses.Requests.Queries;
using GameStore.Application.Interfaces.Infrastructure;
using GameStore.Application.Interfaces.Persistance;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace GameStore.Application.Features.Addresses.RequestHandlers.Queries
{
    public class ReadAddressAllRequestHandler(IUnitOfWork unitOfWork, ILoggerService<ReadAddressAllRequestHandler> loggerService) : IRequestHandler<ReadAddressAllRequest, HttpResponseDto<ReadAddressAllRequest>>
    {
        public async Task<HttpResponseDto<ReadAddressAllRequest>> Handle(ReadAddressAllRequest readAddressAllRequest, CancellationToken cancellationToken)
        {
            var methodName = "HandleReadAddressAllRequest";

            try
            {
                loggerService.LogBeginInformation(methodName, readAddressAllRequest);

                if (readAddressAllRequest == null)
                {
                    return loggerService.LogArgumentNullException<ReadAddressAllRequest>(new ArgumentNullException(nameof(readAddressAllRequest)), methodName);
                }

                var addressDtos = await unitOfWork.AddressRepository.ReadAllAsync(cancellationToken);

                return loggerService.LogDoneInformation(methodName, readAddressAllRequest, addressDtos.Any() ? StatusCodes.Status200OK : StatusCodes.Status204NoContent);
            }
            catch (OperationCanceledException ex)
            {
                return loggerService.LogOperationCanceledException<ReadAddressAllRequest>(ex, methodName);
            }
            catch (Exception ex)
            {
                return loggerService.LogException<ReadAddressAllRequest>(ex, methodName);
            }
        }
    }
}
