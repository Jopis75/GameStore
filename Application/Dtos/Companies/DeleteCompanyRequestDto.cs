﻿using Application.Dtos.Common.Interfaces;

namespace Application.Dtos.Companies
{
    public class DeleteCompanyRequestDto : IDeleteRequestDto
    {
        public int Id { get; set; }
    }
}
