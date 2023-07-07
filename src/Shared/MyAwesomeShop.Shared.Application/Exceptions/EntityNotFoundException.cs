namespace MyAwesomeShop.Shared.Application.Exceptions;

public class ApplicationException : Exception
{
    public ApplicationException(string message) : base(message)
    {
    }
}

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(string entityName, Guid key) : this(entityName, key.ToString())
    {
    }

    public EntityNotFoundException(string entityName, string key) : base($"{entityName} (${key}) не найдена")
    {
    }

    public string EntityName { get; set; }
}