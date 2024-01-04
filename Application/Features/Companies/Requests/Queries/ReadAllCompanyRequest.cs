﻿using Application.Dtos.Common;
using Application.Dtos.Companies;
using MediatR;

namespace Application.Features.Companies.Requests.Queries
{
    public class ReadAllCompanyRequest : IRequest<HttpResponseDto<ReadCompanyResponseDto>>
    {
    }
}
