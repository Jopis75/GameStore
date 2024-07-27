using Domain.Entities;

namespace Domain.Filters
{
    public class TrophyFilter : FilterBase
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public TrophyValue? TrophyValue { get; set; }

        public int? VideoGameId { get; set; }
    }
}
