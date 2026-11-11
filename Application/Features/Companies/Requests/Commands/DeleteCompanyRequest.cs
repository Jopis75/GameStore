using GameStore.Application.Dtos.General;
using MediatR;

namespace GameStore.Application.Features.Companies.Requests.Commands
{
    public class DeleteCompanyRequest : IRequest<HttpResponseDto<DeleteCompanyRequest>>
    {
        public int Id { get; set; }
    }
}
