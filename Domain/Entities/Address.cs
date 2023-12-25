namespace Domain.Entities
{
    public class Address : EntityBase
    {
        public string City { get; set; } = default!;

        public virtual Company? Company { get; set; }

        public string Country { get; set; } = default!;

        public string PostalCode { get; set; } = default!;

        public string State { get; set; } = default!;

        public string StreetAddress { get; set; } = default!;
    }
}
