using Application.Dtos.Common;
using Application.Dtos.Companies;
using MediatR;

namespace Application.Features.Companies.Requests.Queries
{
    public class ReadByIdCompanyRequest : IRequest<HttpResponseDto<ReadCompanyResponseDto>>
    {
        public ReadByIdCompanyRequestDto? ReadByIdCompanyRequestDto { get; set; }
    }
}
