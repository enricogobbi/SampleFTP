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
        static IPEndPoint ipServer = new IPEndPoint(IPAddress.Parse("10.13.100.6"), 60100);
        static TcpListener srv = new TcpListener(ipServer);
        static TcpClient client;
        static string root = /*@"C:\Users\enrico.gobbi\Documents\ftp\"*/@"C:\xampp\htdocs\fileFTP\";

        static void Listening()
        {
            NetworkStream stream;
            byte[] buffer = new byte[2048];
            StreamWriter sw;
            string[] str = new string[2];
            string path;
            int i = 0;
            string tmp = "";


            while (true)
            {
                client = srv.AcceptTcpClient();
                Console.WriteLine("Connesso client");

                //lettura da rete
                stream = client.GetStream();

                
                while ((i = stream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    // Translate data bytes to a ASCII string.
                    tmp += Encoding.ASCII.GetString(buffer, 0, i);

                    //Console.WriteLine("Received: {0}", str);

                    // Process the data sent by the client.
                    /*data = data.ToUpper();

                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

                    // Send back a response.
                    stream.Write(msg, 0, msg.Length);
                    Console.WriteLine("Sent: {0}", data);*/
                }

                /*stream.Read(buffer, 0, buffer.Length);
                stream.Close();*/

                //conversione da byte a ascii
                str = tmp.Split(',');

                //ricavo percorso (dopo la root)
                path = str[0]/*.Substring(str[0].IndexOf(@"\"))*/;
                
                //scrittura del file
                sw = new StreamWriter(root + path);
                sw.Write(str[1]);
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
