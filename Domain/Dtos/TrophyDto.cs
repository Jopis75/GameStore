using Domain.Enums;

namespace Domain.Dtos
{
    public class TrophyDto : DtoBase
    {
        public string Name { get; set; } = default!;

        public string Description { get; set; } = default!;

        public string IconUrl { get; set; } = default!;

        public TrophyValue TrophyValue { get; set; }

        public VideoGameDto VideoGame { get; set; } = new();

        public int VideoGameId { get; set; }
    }
}
