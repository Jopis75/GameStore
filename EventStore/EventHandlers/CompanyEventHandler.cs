using Application.Events.Companies;
using Application.Interfaces.EventSourcing.EventHandlers;
using Application.Interfaces.Infrastructure;
using Application.Interfaces.Persistance;
using AutoMapper;
using Domain.Dtos;
using Microsoft.Extensions.Logging;

namespace EventSourcing.EventHandlers
{
    public class CompanyEventHandler(IUnitOfWork unitOfWork, IMapper mapper, ILoggerService<CompanyEventHandler> loggerService) : ICompanyEventHandler
    {
        public async Task Handle(CompanyCreatedEvent companyCreatedEvent, CancellationToken cancellationToken)
        {
            var methodName = "HandleCompanyCreatedEvent";

            await unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                loggerService.LogBeginInformation(methodName, companyCreatedEvent);

                var companyDto = mapper.Map<CompanyDto>(companyCreatedEvent);
                await unitOfWork.CompanyRepository.CreateAsync(companyDto, cancellationToken);

                await unitOfWork.CommitTransactionAsync(cancellationToken);

                loggerService.LogDoneInformation(methodName, companyCreatedEvent);
            }
            catch (OperationCanceledException ex)
            {
                await unitOfWork.RollbackTransactionAsync(cancellationToken);

                loggerService.LogOperationCanceledException(ex, methodName, companyCreatedEvent);
                throw;
            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackTransactionAsync(cancellationToken);

                loggerService.LogException(ex, methodName, companyCreatedEvent);
                throw;
            }
        }

        public async Task Handle(CompanyDeletedEvent companyDeletedEvent, CancellationToken cancellationToken)
        {
            var methodName = "HandleCompanyDeletedEvent";

            await unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                loggerService.LogBeginInformation(methodName, companyDeletedEvent);

                await unitOfWork.CompanyRepository.DeleteByIdAsync(companyDeletedEvent.Id, cancellationToken);

                await unitOfWork.CommitTransactionAsync(cancellationToken);

                loggerService.LogDoneInformation(methodName, companyDeletedEvent);
            }
            catch (OperationCanceledException ex)
            {
                await unitOfWork.RollbackTransactionAsync(cancellationToken);

                loggerService.LogOperationCanceledException(ex, methodName, companyDeletedEvent);
                throw;
            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackTransactionAsync(cancellationToken);

                loggerService.LogException(ex, methodName, companyDeletedEvent);
                throw;
            }
        }

        public async Task Handle(CompanyUpdatedEvent companyUpdatedEvent, CancellationToken cancellationToken)
        {
            var methodName = "HandleCompanyUpdatedEvent";

            await unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                loggerService.LogBeginInformation(methodName, companyUpdatedEvent);

                var companyDto = mapper.Map<CompanyDto>(companyUpdatedEvent);
                await unitOfWork.CompanyRepository.UpdateAsync(companyDto, cancellationToken);

                await unitOfWork.CommitTransactionAsync(cancellationToken);

                loggerService.LogDoneInformation(methodName, companyUpdatedEvent);
            }
            catch (OperationCanceledException ex)
            {
                await unitOfWork.RollbackTransactionAsync(cancellationToken);

                loggerService.LogOperationCanceledException(ex, methodName, companyUpdatedEvent);
                throw;
            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackTransactionAsync(cancellationToken);

                loggerService.LogException(ex, methodName, companyUpdatedEvent);
                throw;
            }
        }
    }
}
