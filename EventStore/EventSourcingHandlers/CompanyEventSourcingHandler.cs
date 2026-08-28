using Application.Aggregates;
using Application.Interfaces.EventSourcing;
using Application.Interfaces.EventSourcing.EventSourcingHandlers;

namespace EventSourcing.EventSourcingHandlers
{
    public class CompanyEventSourcingHandler(IEventStoreService eventStoreService) : EventSourcingHandlerBase<CompanyAggregate>(eventStoreService), ICompanyEventSourcingHandler
    {
    }
}
