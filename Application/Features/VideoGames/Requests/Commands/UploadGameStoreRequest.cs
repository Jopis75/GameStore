using Application.Dtos.General;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.VideoGames.Requests.Commands
{
    public class UploadGameStoreRequest : IRequest<HttpResponseDto<UploadGameStoreDto>>
    {
    }
}
