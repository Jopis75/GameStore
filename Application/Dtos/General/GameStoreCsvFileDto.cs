using GameStore.Domain.Dtos;

namespace GameStore.Application.Dtos.General
{
    public class GameStoreCsvFileDto
    {
        // Must exist in db.
        public ConsoleDto ConsoleDto { get; set; } = default!;

        // Must exist in db.
        public CompanyDto DeveloperCompanyDto { get; set; } = default!;

        // Must exist in db.
        public List<GenreDto> GenreDtos { get; set; } = [];

        // Must exist in db.
        public CompanyDto PublisherCompanyDto { get; set; } = default!;

        public VideoGameDto VideoGameDto { get; set; } = default!;
    }
}
