using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordCrackerMaster
{
    public class Splitter
    {
        BlockingCollection<List<string>> chunks = new BlockingCollection<List<string>>();
        private void ReadDictionaryAndCreateChunks(string filename)
        {
            List<string> chunk = new List<string>();
            int counter = 0;

            using (FileStream fs = new FileStream("webster-dictionary.txt", FileMode.Open, FileAccess.Read))
            using (StreamReader dictionary = new StreamReader(fs))
            {
                while (!dictionary.EndOfStream)
                {
                    counter++;
                    String word = dictionary.ReadLine();

                    if()

                    //if (counter % 10000 != 0)
                    //{
                    //    chunk.Add(word);
                    //}
                    else
                    {
                        chunks.Add(chunk);
                        chunk = new List<string>();
                    }
                    chunks.Add(chunk);
                }
            }
        }
    }
}
