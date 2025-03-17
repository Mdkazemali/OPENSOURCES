using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace EMS
{
    public class SignalRUserIdProvider: IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            return connection.User?.FindFirst(ClaimTypes.Email)?.Value;
        }
    }
}
