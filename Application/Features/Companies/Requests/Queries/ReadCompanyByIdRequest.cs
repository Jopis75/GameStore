using Application.Dtos.Common;
using Application.Dtos.Companies;
using MediatR;

namespace Application.Features.Companies.Requests.Queries
{
    public class ReadCompanyByIdRequest : IRequest<HttpResponseDto<ReadCompanyResponseDto>>
    {
        public ReadByIdRequestDto? ReadByIdRequestDto { get; set; }
    }
}
