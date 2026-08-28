using Application.Events.Companies;

namespace Application.Interfaces.EventSourcing.EventHandlers
{
    public interface ICompanyEventHandler
    {
        Task Handle(CompanyCreatedEvent companyCreatedEvent, CancellationToken cancellationToken);

        Task Handle(CompanyDeletedEvent companyDeletedEvent, CancellationToken cancellationToken);

        Task Handle(CompanyUpdatedEvent companyUpdatedEvent, CancellationToken cancellationToken);
    }
}
