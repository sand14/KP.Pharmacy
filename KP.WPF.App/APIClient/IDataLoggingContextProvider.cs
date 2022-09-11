using System;

namespace KP.WPF.App.APIClient
{
    public interface IDataLoggingContextProvider
    {
        Guid? GetCurrentUserId();
        string GetCurrentUserName();
        Guid? GetCurrentAreaId();
        string GetCurrentAreaName();
        string GetCurrentModuleName();
    }
}
