namespace Domain.Filters.Interfaces
{
    public interface IFilterBase
    {
        DateTime? CreatedAt { get; }

        string? CreatedBy { get; }

        DateTime? DeletedAt { get; }

        string? DeletedBy { get; }

        int Id { get; }

        DateTime? UpdatedAt { get; }

        string? UpdatedBy { get; }
    }
}
