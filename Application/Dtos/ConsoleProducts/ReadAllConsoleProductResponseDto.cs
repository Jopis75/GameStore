using Application.Dtos.Common.Interfaces;
using Application.Dtos.Consoles;
using Application.Dtos.Products;

namespace Application.Dtos.ConsoleProducts
{
    public class ReadAllConsoleProductResponseDto : ReadAllResponseDto
    {
        public ReadAllConsoleResponseDto? Console { get; set; } = new();

        public DateTime? CreatedAt { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? DeletedAt { get; set; }

        public string? DeletedBy { get; set; }

        public int Id { get; set; }

        public ReadAllProductResponseDto? Product { get; set; } = new();

        public DateTime? UpdatedAt { get; set; }

        public string? UpdatedBy { get; set; }
    }
}
