using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KP.WPF.App.APIClient.RestServices
{
    public class UserRestService : RestServiceBase
    {
        public UserRestService(IHttpClientFactory httpClientFactory, IClientApplicationConfiguration configuration) : base(httpClientFactory, configuration)
        {
        }
    }
}
