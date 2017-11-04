using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riganti.Selenium.Coordinator.Service.Hubs
{
    public class LogHub : Hub
    {

        public static void AddMessage(IHubContext<LogHub> hubContext, string message, bool isError)
        {
            hubContext.Clients.All.InvokeAsync("addMessage", message, isError);
        }

        public static void Refresh(IHubContext<LogHub> hubContext)
        {
            hubContext.Clients.All.InvokeAsync("refresh");
        }
    }
}
