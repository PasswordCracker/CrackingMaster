using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordCrackerMaster
{
    public class Client
    {
        public string IPAdrress { get; set; }
        public bool Ready { get; set; }
        public List<string> Chunk { get; set; }
    }
}
