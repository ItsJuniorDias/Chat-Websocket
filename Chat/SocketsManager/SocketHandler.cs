using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chat.SocketsManager
{
    public class SocketHandler
    {
        public ConnectionManager Connections { get; set; }

        public SocketHandler(ConnectionManager connection)
        {
            Connections = connection;
        }

        public virtual async Task onConnected(WebSocket socket)
        {
            await Task.Run(() => { Connections.AddSocket(socket); });
        }

        public virtual async Task OnDisconnected(WebSocket socket, string message)
        {
            if (socket.State != WebSocketState.Open)
                return;
            await socket.SendAsync(new ArraySegment<byte>(Encoding.ASCII.GetBytes(message), 0, message.Length), 
                WebSocketMessageType.Text, true, CancellationToken.None);
        }
    }
}
