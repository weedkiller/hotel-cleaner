using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace NawafizApp.Web
{
    [HubName("notificationHub")]
    
    public class NotificationHub : Hub
    {
        public NotificationHub()
        {
        }
        public void Send(string name)
        {
            Clients.All.addNewMessageToPage(name);
        }
    }
}