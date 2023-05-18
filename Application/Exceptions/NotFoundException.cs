namespace Application.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string name, object key)
            : base($"Could not found {name} ({key}).")
        {

        }
    }
}
