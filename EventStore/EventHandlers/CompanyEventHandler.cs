using Application.Events.Companies;
using Application.Interfaces.EventSourcing.EventHandlers;
using Application.Interfaces.Persistance;
using AutoMapper;
using Domain.Dtos;
using Microsoft.Extensions.Logging;

namespace EventSourcing.EventHandlers
{
    public class CompanyEventHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CompanyEventHandler> logger) : ICompanyEventHandler
    {
        public async Task Handle(CompanyCreatedEvent companyCreatedEvent, CancellationToken cancellationToken)
        {
            await unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                logger.LogInformation("Begin HandleCompanyCreatedEvent {@CompanyCreatedEvent}.", companyCreatedEvent);

                var companyDto = mapper.Map<CompanyDto>(companyCreatedEvent);
                await unitOfWork.CompanyRepository.CreateAsync(companyDto, cancellationToken);

                await unitOfWork.CommitTransactionAsync(cancellationToken);

                logger.LogInformation("Done HandleCompanyCreatedEvent {@CompanyCreatedEvent}.", companyCreatedEvent);
            }
            catch (OperationCanceledException ex)
            {
                await unitOfWork.RollbackTransactionAsync(cancellationToken);

                logger.LogError(ex, "Canceled HandleCompanyCreatedEvent {@CompanyCreatedEvent}.", companyCreatedEvent);
                throw;
            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackTransactionAsync(cancellationToken);

                logger.LogError(ex, "Error HandleCompanyCreatedEvent {@CompanyCreatedEvent}.", companyCreatedEvent);
                throw;
            }
        }

        public async Task Handle(CompanyDeletedEvent companyDeletedEvent, CancellationToken cancellationToken)
        {
            await unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                logger.LogInformation("Begin HandleCompanyDeletedEvent {@CompanyDeletedEvent}.", companyDeletedEvent);

                await unitOfWork.CompanyRepository.DeleteByIdAsync(companyDeletedEvent.Id, cancellationToken);

                await unitOfWork.CommitTransactionAsync(cancellationToken);

                logger.LogInformation("Done HandleCompanyDeletedEvent {@CompanyDeletedEvent}.", companyDeletedEvent);
            }
            catch (OperationCanceledException ex)
            {
                await unitOfWork.RollbackTransactionAsync(cancellationToken);

                logger.LogError(ex, "Canceled HandleCompanyDeletedEvent {@CompanyDeletedEvent}.", companyDeletedEvent);
                throw;
            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackTransactionAsync(cancellationToken);

                logger.LogError(ex, "Error HandleCompanyDeletedEvent {@CompanyDeletedEvent}.", companyDeletedEvent);
                throw;
            }
        }

        public async Task Handle(CompanyUpdatedEvent companyUpdatedEvent, CancellationToken cancellationToken)
        {
            await unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                logger.LogInformation("Begin HandleCompanyUpdatedEvent {@CompanyUpdatedEvent}.", companyUpdatedEvent);

                var companyDto = mapper.Map<CompanyDto>(companyUpdatedEvent);
                await unitOfWork.CompanyRepository.UpdateAsync(companyDto, cancellationToken);

                await unitOfWork.CommitTransactionAsync(cancellationToken);

                logger.LogInformation("Done HandleCompanyUpdatedEvent {@CompanyUpdatedEvent}.", companyUpdatedEvent);
            }
            catch (OperationCanceledException ex)
            {
                await unitOfWork.RollbackTransactionAsync(cancellationToken);

                logger.LogError(ex, "Canceled HandleCompanyUpdatedEvent {@CompanyUpdatedEvent}.", companyUpdatedEvent);
                throw;
            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackTransactionAsync(cancellationToken);

                logger.LogError(ex, "Error HandleCompanyUpdatedEvent {@CompanyUpdatedEvent}.", companyUpdatedEvent);
                throw;
            }
        }
    }
}
