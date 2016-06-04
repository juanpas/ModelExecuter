using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlock.Web.Notifications
{
    class NotificationsManager
    {
        // Singleton instance
        private readonly static Lazy<NotificationsManager> _instance = new Lazy<NotificationsManager>(
            () => new NotificationsManager(GlobalHost.ConnectionManager.GetHubContext<NotificationsHub>()));

        private static IHubContext _context;

        private NotificationsManager(IHubContext context)
        {
            _context = context;
        }

        public static NotificationsManager Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        public void SendNotification(string text)
        {
            _context.Clients.All.notifyUsers(text);
        }

        public void SendNotification(string userId, string text)
        {
            _context.Clients.User(userId).notifyUser(text);
        }
    }


}
