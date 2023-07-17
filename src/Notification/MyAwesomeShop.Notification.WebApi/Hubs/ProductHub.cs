using Microsoft.AspNetCore.SignalR;
using MyAwesomeShop.Notification.WebApi.Hubs;

internal class ProductHub : Hub
{
    [HubMethodName(ProductHubConstants.SubscribeToProduct)] 
    public async Task SubscribeToProduct(Guid productId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, GetProductGroup(productId));
    }

    [HubMethodName(ProductHubConstants.UnsubscribeFromProduct)]
    public async Task UnsubscribeFromProduct(Guid productId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, GetProductGroup(productId));
    }

    public static string GetProductGroup(Guid productId) =>
        $"product:{productId}";
}