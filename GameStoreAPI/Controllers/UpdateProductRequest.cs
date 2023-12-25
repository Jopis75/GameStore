using Application.Dtos.VideoGames;
using MediatR;

namespace GameStoreAPI.Controllers
{
    internal class UpdateProductRequest : IRequest<object>
    {
        public UpdateVideoGameRequestDto UpdateProductRequestDto { get; set; }
    }
}