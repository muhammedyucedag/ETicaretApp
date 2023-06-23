namespace ETicaretAPI.SignalR.Interface
{
    public interface IOrderHubService
    {
        Task OrderAddedMessageAsync(string message);
    }
}
