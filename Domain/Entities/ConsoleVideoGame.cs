namespace Domain.Entities
{
    public class ConsoleVideoGame : EntityBase
    {
        public virtual Console Console { get; set; } = default!;

        public int ConsoleId { get; set; }

        public virtual VideoGame VideoGame { get; set; } = default!;

        public int VideoGameId { get; set; }
    }
}
