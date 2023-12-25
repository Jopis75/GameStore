namespace Domain.Entities
{
    public class Console : Product
    {
        public virtual ICollection<ConsoleVideoGame> ConsoleVideoGames { get; set; } = new List<ConsoleVideoGame>();
    }
}
