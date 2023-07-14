namespace MyAwesomeShop.Shared.Application.Exceptions;

public class EntityNotFoundException : MyAwesomeShopException
{
    public EntityNotFoundException(string entityName, Guid key) : this(entityName, key.ToString())
    {
    }

    public EntityNotFoundException(string entityName, string key) : base(DefaultTitle, $"{entityName} (${key}) не найден(-а).")
    {
    }

    public string EntityName { get; set; }

    public static string DefaultTitle { get; } = "Cущность не найдена";
}