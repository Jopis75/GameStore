using Application.Events.Addresses;
using Application.Interfaces.EventSourcing.EventHandlers;
using Application.Interfaces.Infrastructure;
using Application.Interfaces.Persistance;
using AutoMapper;
using Domain.Dtos;
using Microsoft.Extensions.Logging;

namespace EventSourcing.EventHandlers
{
    public class AddressEventHandler(IUnitOfWork unitOfWork, IMapper mapper, ILoggerService<AddressEventHandler> loggerService) : IAddressEventHandler
    {
        public async Task Handle(AddressCreatedEvent addressCreatedEvent, CancellationToken cancellationToken)
        {
            var methodName = "HandleAddressCreatedEvent";

            await unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                loggerService.LogBeginInformation<AddressCreatedEvent>(methodName, addressCreatedEvent);

                var addressDto = mapper.Map<AddressDto>(addressCreatedEvent);
                await unitOfWork.AddressRepository.CreateAsync(addressDto, cancellationToken);

                await unitOfWork.CommitTransactionAsync(cancellationToken);

                loggerService.LogDoneInformation<AddressCreatedEvent>(methodName, addressCreatedEvent);
            }
            catch (OperationCanceledException ex)
            {
                await unitOfWork.RollbackTransactionAsync(cancellationToken);

                loggerService.LogOperationCanceledException<AddressCreatedEvent>(ex, methodName, addressCreatedEvent);
                throw;
            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackTransactionAsync(cancellationToken);

                loggerService.LogException<AddressCreatedEvent>(ex, methodName, addressCreatedEvent);
                throw;
            }
        }

        public async Task Handle(AddressDeletedEvent addressDeletedEvent, CancellationToken cancellationToken)
        {
            var methodName = "HandleAddressDeletedEvent";

            await unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                loggerService.LogBeginInformation<AddressDeletedEvent>(methodName, addressDeletedEvent);

                await unitOfWork.AddressRepository.DeleteByIdAsync(addressDeletedEvent.Id, cancellationToken);

                await unitOfWork.CommitTransactionAsync(cancellationToken);

                loggerService.LogDoneInformation<AddressDeletedEvent>(methodName, addressDeletedEvent);
            }
            catch (OperationCanceledException ex)
            {
                await unitOfWork.RollbackTransactionAsync(cancellationToken);

                loggerService.LogOperationCanceledException<AddressDeletedEvent>(ex, methodName, addressDeletedEvent);
                throw;
            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackTransactionAsync(cancellationToken);

                loggerService.LogException<AddressDeletedEvent>(ex, methodName, addressDeletedEvent);
                throw;
            }
        }

        public async Task Handle(AddressUpdatedEvent addressUpdatedEvent, CancellationToken cancellationToken)
        {
            var methodName = "HandleAddressUpdatedEvent";

            await unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                loggerService.LogBeginInformation<AddressUpdatedEvent>(methodName, addressUpdatedEvent);

                var addressDto = mapper.Map<AddressDto>(addressUpdatedEvent);
                await unitOfWork.AddressRepository.UpdateAsync(addressDto, cancellationToken);

                await unitOfWork.CommitTransactionAsync(cancellationToken);

                loggerService.LogDoneInformation<AddressUpdatedEvent>(methodName, addressUpdatedEvent);
            }
            catch (OperationCanceledException ex)
            {
                await unitOfWork.RollbackTransactionAsync(cancellationToken);

                loggerService.LogOperationCanceledException<AddressUpdatedEvent>(ex, methodName, addressUpdatedEvent);
                throw;
            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackTransactionAsync(cancellationToken);

                loggerService.LogException<AddressUpdatedEvent>(ex, methodName, addressUpdatedEvent);
                throw;
            }
        }
    }
}
