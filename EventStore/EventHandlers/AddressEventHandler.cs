using Application.Events.Addresses;
using Application.Interfaces.EventSourcing.EventHandlers;
using Application.Interfaces.Persistance;
using AutoMapper;
using Domain.Dtos;
using Microsoft.Extensions.Logging;

namespace EventSourcing.EventHandlers
{
    public class AddressEventHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<AddressEventHandler> logger) : IAddressEventHandler
    {
        public async Task Handle(AddressCreatedEvent addressCreatedEvent, CancellationToken cancellationToken)
        {
            await unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                logger.LogInformation("Begin HandleAddressCreatedEvent {@AddressCreatedEvent}.", addressCreatedEvent);

                var addressDto = mapper.Map<AddressDto>(addressCreatedEvent);
                await unitOfWork.AddressRepository.CreateAsync(addressDto, cancellationToken);

                await unitOfWork.CommitTransactionAsync(cancellationToken);

                logger.LogInformation("Done HandleAddressCreatedEvent {@AddressCreatedEvent}.", addressCreatedEvent);
            }
            catch (OperationCanceledException ex)
            {
                await unitOfWork.RollbackTransactionAsync(cancellationToken);

                logger.LogError(ex, "Canceled HandleAddressCreatedEvent {@AddressCreatedEvent}.", addressCreatedEvent);
                throw;
            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackTransactionAsync(cancellationToken);

                logger.LogError(ex, "Error HandleAddressCreatedEvent {@AddressCreatedEvent}.", addressCreatedEvent);
                throw;
            }
        }

        public async Task Handle(AddressDeletedEvent addressDeletedEvent, CancellationToken cancellationToken)
        {
            await unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                logger.LogInformation("Begin HandleAddressDeletedEvent {@AddressDeletedEvent}.", addressDeletedEvent);

                await unitOfWork.AddressRepository.DeleteByIdAsync(addressDeletedEvent.Id, cancellationToken);

                await unitOfWork.CommitTransactionAsync(cancellationToken);

                logger.LogInformation("Done HandleAddressDeletedEvent {@AddressDeletedEvent}.", addressDeletedEvent);
            }
            catch (OperationCanceledException ex)
            {
                await unitOfWork.RollbackTransactionAsync(cancellationToken);

                logger.LogError(ex, "Canceled HandleAddressDeletedEvent {@AddressDeletedEvent}.", addressDeletedEvent);
                throw;
            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackTransactionAsync(cancellationToken);

                logger.LogError(ex, "Error HandleAddressDeletedEvent {@AddressDeletedEvent}.", addressDeletedEvent);
                throw;
            }
        }

        public async Task Handle(AddressUpdatedEvent addressUpdatedEvent, CancellationToken cancellationToken)
        {
            await unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                logger.LogInformation("Begin HandleAddressUpdatedEvent {@AddressUpdatedEvent}.", addressUpdatedEvent);

                var addressDto = mapper.Map<AddressDto>(addressUpdatedEvent);
                await unitOfWork.AddressRepository.UpdateAsync(addressDto, cancellationToken);

                await unitOfWork.CommitTransactionAsync(cancellationToken);

                logger.LogInformation("Done HandleAddressUpdatedEvent {@AddressUpdatedEvent}.", addressUpdatedEvent);
            }
            catch (OperationCanceledException ex)
            {
                await unitOfWork.RollbackTransactionAsync(cancellationToken);

                logger.LogError(ex, "Canceled HandleAddressUpdatedEvent {@AddressUpdatedEvent}.", addressUpdatedEvent);
                throw;
            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackTransactionAsync(cancellationToken);

                logger.LogError(ex, "Error HandleAddressUpdatedEvent {@AddressUpdatedEvent}.", addressUpdatedEvent);
                throw;
            }
        }
    }
}
