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

        }

        
        }
    }
}
