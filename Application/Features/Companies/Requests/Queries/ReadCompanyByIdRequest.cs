﻿using Application.Dtos.Common;
using Domain.Dtos;
using MediatR;

namespace Application.Features.Companies.Requests.Queries
{
    public class ReadCompanyByIdRequest : IRequest<HttpResponseDto<CompanyDto>>
    {
        public int? Id { get; set; }
    }
}
