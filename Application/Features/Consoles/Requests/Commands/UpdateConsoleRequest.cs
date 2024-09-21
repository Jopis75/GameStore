using Application.Dtos.General;
using Domain.Dtos;
using MediatR;

namespace Application.Features.Consoles.Requests.Commands
{
    public class UpdateConsoleRequest : IRequest<HttpResponseDto<ConsoleDto>>
    {
        public int DeveloperId { get; set; }

        public string? ImageUri { get; set; }

        public string Name { get; set; } = String.Empty;

        public decimal Price { get; set; }

        public DateTime PurchaseDate { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string? Url { get; set; }
    }
}
