using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PasswordCrackerMaster
{
    public class Program
    {
        static void Main(string[] args)
        {
            TcpListener listener = new TcpListener(System.Net.IPAddress.Loopback, 10001);
            listener.Start();
            Console.WriteLine("Master is here");

            while (true)
            {
                TcpClient socket = listener.AcceptTcpClient();
                Task.Run(() =>
                {
                    HandleClient(socket);
                });
            }
        }



        public static void HandleClient(TcpClient socket)
        {
            NetworkStream ns = socket.GetStream();
            StreamReader reader = new StreamReader(ns);
            StreamWriter writer = new StreamWriter(ns);

            Console.WriteLine("Slave is here");

            string message = reader.ReadLine();
            Console.WriteLine("Slave wrote " + message);
            writer.WriteLine(message);
            writer.Flush();


            while(true)
            {
                string command = reader.ReadLine();
                string parameter = reader.ReadLine();
                if(command == "SendPasswords" && string.IsNullOrEmpty(parameter))
                {

                }

                else
                {
                    writer.WriteLine("Invalid command, valid commands are: ");
                    writer.Flush();
                }
            }


        }

        
        }
    }
}
