using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using SignalRServer.Models;
using System.Collections.Concurrent;
using System.Security.Cryptography;
using System.Text.Json;
using System.Threading.Tasks;

namespace SignalRServer.Hubs
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    //[Authorize]
    [AllowAnonymous]
    public class ProgressHub2:Hub
    {

        public override async Task OnConnectedAsync()
        {
            string UserId = Context.GetHttpContext().Request.Query["UserId"].ToString();
            string UserName = Context.GetHttpContext().Request.Query["UserName"].ToString();
            await Clients.Client(Context.ConnectionId).SendAsync("UpdateInit", Context.ConnectionId, UserId, UserName);

            await base.OnConnectedAsync();

        }
        public async Task NewMessage(long username, string message) =>
        await Clients.All.SendAsync("messageReceived", username, message);

        public async Task SendMessage(string UserId, string UserName, string RoomId, string Message)
        {

            ChatMessage chatMessage = new ChatMessage();
            chatMessage.UserId = UserId;
            chatMessage.RoomId = RoomId;
            chatMessage.Message = Message;
            chatMessage.UpdateDate = DateTime.Now;
           // _chatMessageService.InsertAsync(chatMessage);

            var resultContent = new
            {
                UserId = UserId,
                UserName = UserName,
                Message = Message
            };
            await Clients.All.SendAsync("UadateContent", JsonConvert.SerializeObject(resultContent));
        }
    }

}
