using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;

namespace PasswordCrackerMaster
{
    public class Program
    {
        static List<Client> clientList = new List<Client>();
        public static List<Client> readyList = new List<Client>();
        static void Main(string[] args)
        {

            TcpListener listener = new TcpListener(System.Net.IPAddress.Loopback, 10001);
            listener.Start();
            Console.WriteLine("Master is here");

            while (true)
            {
                if (listener.Pending())
                {
                    TcpClient socket = listener.AcceptTcpClient();


                    Task.Run(() =>
                    {
                        HandleClient(socket);

                    });
                }

            }
        }



        public static void HandleClient(TcpClient socket)
        {
            NetworkStream ns = socket.GetStream();
            StreamReader reader = new StreamReader(ns);
            StreamWriter writer = new StreamWriter(ns);
            Client c = new Client();
            c.IPAdrress = socket.Client.RemoteEndPoint.ToString();
            c.Ready = false;
            clientList.Add(c);
            Console.WriteLine("New slave is here");

            string message = reader.ReadLine();
            Console.WriteLine("Slave wrote " + message);
            if (message == "Ready")
            {
                c.Ready = true;
                readyList.Add(c);
                writer.WriteLine($"{readyList.Count} out of {clientList.Count} clients are ready");
                writer.Flush();
            }
            if (clientList.Count == readyList.Count)
            {
                foreach(Client in readyList)
                writer.WriteLine("ready");
                Console.WriteLine("All slaves are ready, type start to commence cracking");
                string command = Console.ReadLine();
                if (command == "start")
                {
                    string jsonstring = JsonSerializer.Serialize();
                }
            }
        }

        public static void ComparingClientCount()
        {

        }
    }
}
