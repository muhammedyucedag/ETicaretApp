namespace ETicaretAPI.SignalR
{
    public interface IProductHubService
    {
        Task ProductAddedMessageAsync(string message);
    }
}
