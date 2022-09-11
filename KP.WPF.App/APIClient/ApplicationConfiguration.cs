using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KP.WPF.App.APIClient
{
    public class ApplicationConfiguration : IClientApplicationConfiguration
    {
        public string ServerAddress => "https://localhost:7179";

        public string AppUri => "AppUri";

        public string ClientId => "ClientId";
    }
}
