using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

namespace ModelExecuter.Web.Notifications
{
    public class NotificationsHub : Hub
    {
        public void Hello()
        {
            Clients.All.notifyUsers("Hello!!");
        }

    }
}