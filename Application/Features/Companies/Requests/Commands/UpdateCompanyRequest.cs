using Application.Dtos.Companies;
using Application.Dtos.Common;
using MediatR;

namespace Application.Features.Companies.Requests.Commands
{
    public class UpdateCompanyRequest : IRequest<HttpResponseDto<UpdateCompanyResponseDto>>
    {
        public UpdateCompanyRequestDto? UpdateCompanyRequestDto { get; set; }
    }
}
