namespace Telegram.Bot.Examples.Polling.Abstract;

/// <summary>
/// A marker interface for Update Receiver service
/// </summary>
public interface IReceiverService
{
    Task ReceiveAsync(CancellationToken stoppingToken);
}