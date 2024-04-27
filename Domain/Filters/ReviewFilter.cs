namespace Domain.Filters
{
    public class ReviewFilter : FilterBase
    {
        public int? Grade { get; set; }

        public DateTime? ReviewDate { get; set; }
    }
}
