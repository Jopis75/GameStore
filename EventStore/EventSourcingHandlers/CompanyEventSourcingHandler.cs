using GameStore.Application.Aggregates;
using GameStore.Application.Interfaces.EventSourcing;
using GameStore.Application.Interfaces.EventSourcing.EventSourcingHandlers;

namespace GameStore.EventSourcing.EventSourcingHandlers
{
    public class CompanyEventSourcingHandler(IEventStoreService eventStoreService) : EventSourcingHandlerBase<CompanyAggregate>(eventStoreService), ICompanyEventSourcingHandler
    {
    }
}
