using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace FTP_Server
{
    class Program
    {
        static IPEndPoint ipServer = new IPEndPoint(IPAddress.Parse("10.13.0.23"), 60100);
        static TcpListener srv = new TcpListener(ipServer);

        static void Listening()
        {
            while (true)
            {
                srv.Start();
                srv.AcceptTcpClient();

                
            }
        }

        static void Main(string[] args)
        {
            Thread th = new Thread(Listening);
            th.Start();
        }
    }
}
