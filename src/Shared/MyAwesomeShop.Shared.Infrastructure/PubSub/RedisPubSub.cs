using System.Text.Json;

using Microsoft.Extensions.Options;

using MyAwesomeShop.Shared.Application.Events;

using StackExchange.Redis;

namespace MyAwesomeShop.Shared.Infrastructure.PubSub;

internal class RedisPubSub : IPubSub
{
    private readonly ConnectionMultiplexer _connection;

    private readonly IPubSubDispatcher _dispatcher;

    public RedisPubSub(IOptions<PubSubOptions> options, IPubSubDispatcher dispatcher)
    {
        _connection = ConnectionMultiplexer.Connect(options.Value.Configuration);
        _dispatcher = dispatcher;
    }

    public Task PublishAsync<T>(T message) where T : IEvent
	{
		var sub = _connection.GetSubscriber();

		return sub.PublishAsync(RedisChannel.Literal(nameof(T)), JsonSerializer.Serialize(message));
	}

    public async Task SubscribeAsync<T>() where T : IEvent
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
