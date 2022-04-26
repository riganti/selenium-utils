using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Riganti.Selenium.Coordinator.Service.Hubs
{
    public class LogHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            await this.Clients.All.SendAsync("addMessage", "Client connected");
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

        public static void AddMessage(IHubContext<LogHub> hubContext, string message, bool isError)
        {
            hubContext.Clients.All.SendAsync("addMessage", message, isError);
        }

        public static void Refresh(IHubContext<LogHub> hubContext)
        {
            hubContext.Clients.All.SendAsync("refresh");
        }
    }
}
