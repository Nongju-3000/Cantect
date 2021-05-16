using System.Net.Sockets;
using System.Threading;
using System.Net;
using System;
using WindowProject;

namespace WindowProject
{
    public static class SetupServer
    {
        static Socket lis_sock;
        static public event RecvRCInfoEventHandler RecvRCInfo = null;
        static string ip;
        static int port;

        public static void Start(string ip, int port)
        {
            SetupServer.ip = ip;
            SetupServer.port = port;
            SocketBooting();
        }

        private static void SocketBooting()
        {
            IPAddress ipaddr = IPAddress.Parse(ip);
            IPEndPoint ep = new IPEndPoint(ipaddr, port);
            lis_sock = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
            lis_sock.Bind(ep);
            lis_sock.Listen(1);
            lis_sock.BeginAccept(DoAccept, null);
        }
        static void DoAccept(IAsyncResult result)
        {
            if (lis_sock == null)
            {
                return;
            }
            try
            {
                Socket sock = lis_sock.EndAccept(result);
                DoIt(sock);
                lis_sock.BeginAccept(DoAccept, null);
            }
            catch
            {
                Close();
            }
        }
        static void DoIt(Socket dosock)
        {
            if(RecvRCInfo != null)
            {
                RecvRCInfoEventArgs e = new RecvRCInfoEventArgs(dosock.RemoteEndPoint);
                RecvRCInfo(null, e); // sender = null
            }
            dosock.Close();
        }
        public static void Close()
        {
            if (lis_sock != null)
            {
                lis_sock.Close();
                lis_sock = null;
            }
        }
    }
}
