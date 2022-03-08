using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PasswordCrackerMaster
{
    public class Client
    {
        public string IPAdrress { get; set; }
        public bool Ready { get; set; }
        public TcpClient Socket { get; set; }
        public StreamWriter Writer { get; set; }
        public StreamReader Reader { get; set; }
        public bool HasChunk { get; set; }
        public bool awaitsResponse { get; set; }
    }
}
