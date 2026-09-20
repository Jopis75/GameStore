using Application.Aggregates;

namespace Application.Interfaces.EventSourcing.EventSourcingHandlers
{
    public interface ICompanyEventSourcingHandler : IEventSourcingHandlerBase<CompanyAggregate>
    {
    }
}
