namespace GameStore.Domain.Entities
{
    public class Console : Product
    {
        public virtual ICollection<ConsoleVideoGame> ConsoleVideoGames { get; set; } = [];
    }
}
