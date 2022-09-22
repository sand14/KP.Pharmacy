namespace KP.WPF.App.APIClient
{
    public interface IClientApplicationConfiguration
    {
        string ServerAddress { get; }
        string AppUri { get; }
        string ClientId { get; }
    }
}
