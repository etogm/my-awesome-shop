namespace MyAwesomeShop.UI.BlazorServer.Data;

public class ProductService
{
    private readonly HttpClient _httpClient;

    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<PaginatedList<Product>?> GetProductsAsync()
    {
        var res = await _httpClient.GetFromJsonAsync<PaginatedList<Product>>("");
        return res;
    }
}

public class PaginatedList<T> where T : class
{
    public IEnumerable<T> Items { get; set; }

    public int CurrentPage { get; set; }

    public int TotalPages { get; set; }

    public int TotalCount { get; set; }
}
