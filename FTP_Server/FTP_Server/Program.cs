using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace FTP_Server
{
    class Program
    {
        static IPEndPoint ipServer = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 60100);
        static TcpListener srv = new TcpListener(ipServer);
        static TcpClient client;
        static string root = /*"C:/Users/Administrator/Desktop/ftp"*/@"C:\Users\Utente ASUS\Documents\SampleFTP\";

        static void Listening()
        {
            NetworkStream stream;
            byte[] buffer = new byte[2048];
            StreamWriter sw;
            string[] str;
            string path;

            while (true)
            {
                client = srv.AcceptTcpClient();
                Console.WriteLine("Connesso client");

                //lettura da rete
                stream = client.GetStream();
                stream.Read(buffer, 0, buffer.Length);
                stream.Close();

                //conversione da byte a ascii
                str = Encoding.ASCII.GetString(buffer).Split(',');

                //ricavo percorso (dopo la root)
                path = str[0]/*.Substring(str[0].IndexOf(@"\"))*/;
                
                //scrittura del file
                sw = new StreamWriter(root + path);
                sw.Write(str);
                sw.Flush();
                sw.Close();
            }
        }

        static void Main(string[] args)
        {
            Thread th = new Thread(Listening);

            Console.WriteLine("Start");
            srv.Start();

            Console.WriteLine("Attesa connessioni...");
            th.Start();
        }
    }
}
