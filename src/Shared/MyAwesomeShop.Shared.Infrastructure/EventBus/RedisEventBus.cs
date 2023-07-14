using System.Text.Json;

using Microsoft.Extensions.Options;

using MyAwesomeShop.Shared.Application.IntegrationEvent;

using StackExchange.Redis;

namespace MyAwesomeShop.Shared.Infrastructure.EventBus;

internal class RedisEventBus : IEventBus
{
    private readonly ConnectionMultiplexer _connection;

    private readonly IEventBusDispatcher _dispatcher;

    public RedisEventBus(IOptions<EventBusOptions> options, IEventBusDispatcher dispatcher)
    {
        _connection = ConnectionMultiplexer.Connect(options.Value.Configuration);
        _dispatcher = dispatcher;
    }

    public Task PublishAsync<T>(T message) where T : IntegrationEvent
    {
        var sub = _connection.GetSubscriber();

        return sub.PublishAsync(RedisChannel.Literal(nameof(T)), JsonSerializer.Serialize(message));
    }

    public async Task SubscribeAsync<T>() where T : IntegrationEvent
    {
        var sub = _connection.GetSubscriber();

        await sub.SubscribeAsync(RedisChannel.Literal(nameof(T)), async (channel, message) =>
        {
            if (!message.HasValue)
            {
                return;
            }

            var deserialize = JsonSerializer.Deserialize<T>(message.ToString());

            if (deserialize == null)
            {
                return;
            }

            await _dispatcher.DispatchAsync(deserialize);
        });
    }
}
