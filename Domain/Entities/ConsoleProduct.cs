namespace Domain.Entities
{
    public class ConsoleProduct : EntityBase
    {
        public virtual Console? Console { get; set; }

        public int? ConsoleId { get; set; }

        public virtual Product? Product { get; set; }

        public int? ProductId { get; set; }
    }
}
