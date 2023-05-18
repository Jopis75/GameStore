namespace Domain.Entities
{
    public class Address : EntityBase
    {
        public string? City { get; set; }

        public virtual Company? Company { get; set; }

        public int? CompanyId { get; set; }

        public string? Country { get; set; }

        public string? PostalCode { get; set; }

        public string? State { get; set; }

        public string? StreetAddress { get; set; }
    }
}
