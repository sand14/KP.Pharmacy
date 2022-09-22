namespace KP.WPF.App.APIClient
{
    public class ApplicationConfiguration : IClientApplicationConfiguration
    {
        public string ServerAddress => "https://localhost:7179";

        public string AppUri => "AppUri";

        public string ClientId => "ClientId";
    }
}
