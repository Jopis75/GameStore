﻿using Application.Dtos.Common.Interfaces;

namespace Application.Dtos.ConsoleProducts
{
    public class UpdateConsoleProductRequestDto : UpdateRequestDto
    {
        public int? ConsoleId { get; set; }

        public int Id { get; set; }

        public int? ProductId { get; set; }
    }
}
