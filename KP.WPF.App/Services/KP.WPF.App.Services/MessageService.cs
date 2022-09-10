using KP.WPF.App.Services.Interfaces;

namespace KP.WPF.App.Services
{
    public class MessageService : IMessageService
    {
        public string GetMessage()
        {
            return "Hello from the Message Service";
        }
    }
}
