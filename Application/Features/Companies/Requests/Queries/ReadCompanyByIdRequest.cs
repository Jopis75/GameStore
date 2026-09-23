using Application.Dtos.General;
using MediatR;

namespace Application.Features.Companies.Requests.Queries
{
    public class ReadCompanyByIdRequest : IRequest<HttpResponseDto<ReadCompanyByIdRequest>>
    {
        public int Id { get; set; }
    }
}
