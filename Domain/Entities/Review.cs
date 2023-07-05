﻿namespace Domain.Entities
{
    public class Review : EntityBase
    {
        public virtual Console? Console { get; set; }

        public int? ConsoleId { get; set; }

        public int? Grade { get; set; }

        public DateTime? ReviewDate { get; set; }

        public string? ReviewText { get; set; }

        public virtual Product? Product { get; set; }

        public int? ProductId { get; set; }
    }
}
