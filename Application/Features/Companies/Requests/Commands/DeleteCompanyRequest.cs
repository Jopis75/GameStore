using Application.Dtos.Common;
using Domain.Dtos;
using MediatR;

namespace Application.Features.Companies.Requests.Commands
{
    public class DeleteCompanyRequest : IRequest<HttpResponseDto<CompanyDto>>
    {
        public int? Id { get; set; }
    }
}
