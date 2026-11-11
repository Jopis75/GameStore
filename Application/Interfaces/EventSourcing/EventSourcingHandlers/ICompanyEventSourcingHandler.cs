using GameStore.Application.Aggregates;

namespace GameStore.Application.Interfaces.EventSourcing.EventSourcingHandlers
{
    public interface ICompanyEventSourcingHandler : IEventSourcingHandlerBase<CompanyAggregate>
    {
    }
}
