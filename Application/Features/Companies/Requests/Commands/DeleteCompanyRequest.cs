using Application.Dtos.Companies;
using Application.Dtos.Common;
using MediatR;

namespace Application.Features.Companies.Requests.Commands
{
    public class DeleteCompanyRequest : IRequest<HttpResponseDto<DeleteCompanyResponseDto>>
    {
        public DeleteCompanyRequestDto? DeleteCompanyRequestDto { get; set; }
    }
}
