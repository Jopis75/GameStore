﻿using Application.Dtos.Common;
using Application.Dtos.VideoGames;
using MediatR;

namespace Application.Features.VideoGames.Requests.Queries
{
    public class ReadVideoGameAllRequest : IRequest<HttpResponseDto<List<ReadVideoGameResponseDto>>>
    {
    }
}
