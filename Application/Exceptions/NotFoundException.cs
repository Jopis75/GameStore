namespace Application.Exceptions
{
    // ToDo: Fix error message.
    public class NotFoundException(string name, object key) : ApplicationException($"Could not found {name} ({key}).")
    {
    }
}
