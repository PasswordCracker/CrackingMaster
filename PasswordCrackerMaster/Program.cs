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
        static int readyclients = 0;
        public static List<TcpClient> testList = new List<TcpClient>(); //what is this for?
        static bool waitingforclients = true; //if this is false, the master will stop accepting new clients
        static List<List<string>> ListOfChunks = new List<List<string>>();
        static List<Client> hasNoChunk = new List<Client>();
        static void Main(string[] args)
        {
            TcpListener listener = new TcpListener(IPAddress.Any, 21);
            listener.Start();
            Console.WriteLine("Master is here");
            Console.WriteLine("Awaiting clients");

            Task.Run(() =>
            {
                 ListOfChunks = Splitter.ReadDictionaryAndCreateChunks(Helper.ReadHelper.ReadDictionary());
            });

            while (waitingforclients)
            {
                if (listener.Pending())
                {
                    TcpClient socket = listener.AcceptTcpClient();

                    Task.Run(() =>
                    {
                        HandleClient(socket);
                        AwaitMessage(socket);
                        if (readyclients == clientList.Count && clientList.Count >= 1)
                        {
                            PromptForStarting();
                        }
                    });
                }
            }
            //send out chunks and receive results
            while(ListOfChunks.Count > 0)
            {
                Task.Run(() =>
                {
                    CheckClients();
                    HandOutChunks();
                    foreach(Client c in clientList)
                    {
                        AwaitResults(c);
                    }
                });
            }
            //tell all clients the work is finished
        }

        //accepts clients and adds them to the list
        public static void HandleClient(TcpClient socket)
        {
            testList.Add(socket);

            Client c = new Client();
            NetworkStream ns = socket.GetStream();
            StreamReader reader = new StreamReader(ns);
            StreamWriter writer = new StreamWriter(ns);

            c.IPAdrress = socket.Client.RemoteEndPoint.ToString();
            c.Ready = false;
            c.Socket = socket;
            c.Reader = reader;
            c.Writer = writer;
            clientList.Add(c);
            Console.WriteLine($"New client is here, {clientList.Count} in total");
        }
        //waits for the ready message from each client
        public static void AwaitMessage(TcpClient socket)
        {
            NetworkStream ns = socket.GetStream();
            StreamReader reader = new StreamReader(ns);
            StreamWriter writer = new StreamWriter(ns);
            if (reader.ReadLine() == "ready")
            {
                readyclients++;
                string message = $"{readyclients} out of {clientList.Count} clients are ready";
                Console.WriteLine(message);
                //informs each client of the ready status
                foreach (Client c in clientList)
                {
                    c.Writer.WriteLine(message);
                    c.Writer.Flush();
                }
                
            }
        }
        //prompts the user to start the cracking if all the clients are ready
        //gets interrupted if another client joins in the process
        //works a bit too well honestly 
        public static void PromptForStarting()
        {
            string command = "";
            Console.WriteLine("All clients are ready. Type 'start' to begin cracking.");
            command = Console.ReadLine();
            while (command.ToLower() != "start" && readyclients == clientList.Count)
            {
                Console.WriteLine("Unknown command, type 'start' to being cracking.");
                command = Console.ReadLine();
            }
            if (command.ToLower() == "start")
            {
                waitingforclients = false;
            }
        }
        //checks which client does not have a chunk
        public static void CheckClients()
        {
            foreach (Client c in clientList)
            {
                if(!c.HasChunk) hasNoChunk.Add(c);
            }
        }
        //gives a chunk to all the clients that do not have one
        public static void HandOutChunks()
        {
            foreach(Client c in hasNoChunk)
            {
                List<string> chunk = ListOfChunks[ListOfChunks.Count];
                ListOfChunks.RemoveAt(ListOfChunks.Count);
                string jsonchunk = JsonSerializer.Serialize(chunk);
                c.Writer.WriteLine(jsonchunk);
                c.Writer.Flush();
            }
        }
        public static void AwaitResults(Client client)
        {
            string jsonresult = client.Reader.ReadToEnd();
            Dictionary<string, string> result = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonresult);
            foreach(KeyValuePair<string, string> kvp in result)
            {
                Console.WriteLine(kvp.Key + " : " + kvp.Value);
            }
            //add this result to the file
            client.HasChunk = false;
        }
        //public static void Aux()
        //{ 
        //    string message = reader.ReadLine();
        //    Console.WriteLine("Client wrote " + message);


        //    if (message.ToLower() == "ready")
        //    {
        //        c.Ready = true;
        //        readyList.Add(c);
        //        Console.WriteLine($"{readyclients} out of {clientList.Count} clients are ready");
        //        writer.Flush();
        //    }
        //    if (clientList.Count == readyList.Count)
        //    {
        //        //sends the start command to every client that is ready
        //        foreach (var client in readyList)   //Not sure if correct, but now it can process multiple clients... i think
        //        {
        //            writer.WriteLine("ready");
        //            Console.WriteLine("All clients are ready, type start to commence cracking");
        //            string command = Console.ReadLine();
        //            if (command.ToLower() == "start")
        //            {
        //                string jsonstring = JsonSerializer.Serialize(command);
        //                writer.WriteLine(jsonstring);
        //                writer.Flush();

        //            }
        //            else Console.WriteLine("Invalid command, try 'start' ");
        //        }

        //        int ct = 0;
        //        List<List<string>> ListOfChunks = Splitter.ReadDictionaryAndCreateChunks(ReadHelper.ReadDictionary());
                
                
        //        //Task.Run somewhere here
                
        //        foreach (var client in testList)
        //        {
        //            //while (ct != readyList.Count)
        //            if(ct!=readyList.Count)
        //            {
                       
        //                StreamWriter writerNew = new StreamWriter(client.GetStream());

        //                string jsonstring = JsonSerializer.Serialize(ListOfChunks[ct]);
        //                writerNew.WriteLine(jsonstring);
        //                writerNew.WriteLine("&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&");
        //                writerNew.Flush();
        //                ct++;
        //            }
                    
                    
                    
        //        }

        //    }
        //}

    }
}
