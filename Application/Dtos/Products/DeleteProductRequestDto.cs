using Application.Dtos.Common.Interfaces;

namespace Application.Dtos.Products
{
    public class DeleteProductRequestDto : IDeleteRequestDto
    {
        public int Id { get; set; }
    }
}
