using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System;

namespace WinFormsApp1
{
    public class ClientService
    {
        private const int DEFAULT_BUFLEN = 512;
        private const string DEFAULT_PORT = "27015";
        private Socket clientSocket;

        public async Task<bool> ConnectToServerAsync()
        {
            var ipAddress = IPAddress.Loopback;
            var remoteEndPoint = new IPEndPoint(ipAddress, int.Parse(DEFAULT_PORT));
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                await clientSocket.ConnectAsync(remoteEndPoint);
                return true;
            }
            catch (SocketException)
            {
                try
                {
                    Process.Start("C:\\Users\\USER\\Desktop\\Новая папка (2)\\Server\\bin\\Debug\\net9.0\\Server.exe");
                    await Task.Delay(2000);
                    await clientSocket.ConnectAsync(remoteEndPoint);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public async Task<string> SendRequestAsync(string message)
        {
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            await clientSocket.SendAsync(new ArraySegment<byte>(messageBytes), SocketFlags.None);

            var buffer = new byte[DEFAULT_BUFLEN];
            int bytesReceived = await clientSocket.ReceiveAsync(new ArraySegment<byte>(buffer), SocketFlags.None);
            return Encoding.UTF8.GetString(buffer, 0, bytesReceived);
        }

        public void Close()
        {
            if (clientSocket?.Connected == true)
            {
                clientSocket.Shutdown(SocketShutdown.Send);
                clientSocket.Close();
            }
        }
    }
}
