using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KP.WPF.App.APIClient
{
    public interface IClientApplicationConfiguration
    {
        string ServerAddress { get; }
        string AppUri { get; }
        string ClientId { get; }
    }
}
