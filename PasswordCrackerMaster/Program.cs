using PasswordCrackerMaster.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Text;
using System.Threading.Tasks;

namespace PasswordCrackerMaster
{
    public class Program
    {
        static List<Client> clientList = new List<Client>();
        public static List<Client> readyList = new List<Client>();
        public static List<TcpClient> testList = new List<TcpClient>();
        //public static List<Client> sentList = new List<Client>();
        static void Main(string[] args)
        {
            TcpListener listener = new TcpListener(IPAddress.Any, 10000);
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
            testList.Add(socket);

            Client c = new Client();
            NetworkStream ns = socket.GetStream();
            StreamReader reader = new StreamReader(ns);
            StreamWriter writer = new StreamWriter(ns);

            c.IPAdrress = socket.Client.RemoteEndPoint.ToString();
            c.Ready = false;
            clientList.Add(c);
            Console.WriteLine("New slave is here");

            string message = reader.ReadLine();
            Console.WriteLine("Slave wrote " + message);


            if (message == "ready")
            {
                c.Ready = true;
                readyList.Add(c);
                Console.WriteLine($"{readyList.Count} out of {clientList.Count} clients are ready");
                writer.Flush();
            }
            if (clientList.Count == readyList.Count)
            {
                //sends the start command to every slave that is ready
                foreach (var client in readyList)   //Not sure if correct, but now it can process multiple slaves... i think
                {
                    writer.WriteLine("ready");
                    Console.WriteLine("All slaves are ready, type start to commence cracking");
                    string command = Console.ReadLine();
                    if (command == "start")
                    {
                        string jsonstring = JsonSerializer.Serialize(command);
                        writer.WriteLine(jsonstring);
                        writer.Flush();

                    }
                    else Console.WriteLine("Invalid command, try 'start' ");
                }

                int ct = 0;
                var chunk = Splitter.ReadDictionaryAndCreateChunks(ReadHelper.ReadDictionary());
                
                
                //Task.Run somewhere here
                
                foreach (var client in testList)
                {
                    //while (ct != readyList.Count)
                    if(ct!=readyList.Count)
                    {
                       
                        StreamWriter writerNew = new StreamWriter(client.GetStream());

                        string jsonstring = JsonSerializer.Serialize(chunk[ct]);
                        writerNew.WriteLine(jsonstring);
                        writerNew.WriteLine("&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&");
                        writerNew.Flush();
                        ct++;
                    }
                    
                    
                    
                }

            }
        }

    }
}
