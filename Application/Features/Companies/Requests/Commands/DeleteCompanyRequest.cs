using Application.Dtos.General;
using MediatR;

namespace Application.Features.Companies.Requests.Commands
{
    public class DeleteCompanyRequest : IRequest<HttpResponseDto<DeleteCompanyRequest>>
    {
        public int Id { get; set; }
    }
}
