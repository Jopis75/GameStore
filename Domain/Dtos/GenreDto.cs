using Domain.Entities;

namespace Domain.Dtos
{
    public class GenreDto : DtoBase
    {
        public string Name { get; set; } = default!;

        public string? Description { get; set; }

        public List<VideoGameGenre> VideoGameGenres { get; set; } = new();
    }
}
