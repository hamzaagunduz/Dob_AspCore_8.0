using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Hubs
{
    public class PayHub : Hub
    {
        public static readonly IDictionary<string, string> TransactionConnections = new Dictionary<string, string>();

        public void RegisterTransaction(string id)
        {
            var connectionId = Context.ConnectionId;
            TransactionConnections[id] = connectionId;
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var connectionId = Context.ConnectionId;
            var item = TransactionConnections.FirstOrDefault(p => p.Value == connectionId);
            TransactionConnections.Remove(connectionId);
            return base.OnDisconnectedAsync(exception);
        }
    }
}
