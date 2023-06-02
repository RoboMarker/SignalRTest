using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using System.Security.Cryptography;
using System.Text.Json;
using System.Threading.Tasks;

namespace SignalRServer.Hubs
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize]
    public class ProgressHub:Hub
    {

        public override async Task OnConnectedAsync()
        {
            await Clients.Client(Context.ConnectionId).SendAsync("SetHubConnId", Context.ConnectionId);

            await base.OnConnectedAsync();
        }
        public async Task NewMessage(long username, string message) =>
        await Clients.All.SendAsync("messageReceived", username, message);
    }
}
