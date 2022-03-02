using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using PasswordCrackerMaster.Helper;

namespace PasswordCrackerMaster
{
    public class Splitter
    {
        #region v2

        //private bool ChunkIsFull = false;
        //public int WordCount = 10000;
        //static int ReadyClients = Program.readyList.Count;
        //BlockingCollection<List<string>> chunks = new BlockingCollection<List<string>>();
        //private void CreateChunks(List<string> wordDictList)
        //{
        //    List<string> chunk = new List<string>();
        //    while ()
        //        ReadHelper.ReadDictionary().Count / WordCount



        //    #region v1

        //    //if (wordDictList.Count % ClientsReady == 0)
        //    //{
        //    //    counter = wordDictList.Count / ClientsReady;
        //    //}
        //    //else
        //    //{
        //    //    counter = wordDictList.Count / ClientsReady + 1;
        //    //}




        //    //using (FileStream fs = new FileStream("webster-dictionary.txt", FileMode.Open, FileAccess.Read))
        //    //using (StreamReader dictionary = new StreamReader(fs))
        //    //{
        //    //    while (!dictionary.EndOfStream)
        //    //    {
        //    //        counter++;
        //    //        String word = dictionary.ReadLine();

        //    //        if()
        //    //        {
        //    //            chunk.Add(word);
        //    //        }

        //    //        //if (counter % 10000 != 0)
        //    //        //{
        //    //        //    chunk.Add(word);
        //    //        //}
        //    //        else
        //    //        {
        //    //            chunks.Add(chunk);
        //    //            chunk = new List<string>();
        //    //        }
        //    //        chunks.Add(chunk);
        //    //    }
        //    //}


        //    #endregion
        //}

        #endregion
        //static int ClientsReady = 5;
        private static int ClientsReady = Program.readyList.Count;
        const int WordsPerChunk = 1000;
        public static List<List<string>> ReadDictionaryAndCreateChunks(List<string> wordDictList)
        {
            List<List<string>> ListOfChunks = new List<List<string>>();
            int wordindex = 1;
            while (wordindex < wordDictList.Count)
            {
                List<string> Chunk = new List<string>();
                int counter = 1;
                while (counter < WordsPerChunk && wordindex < wordDictList.Count)
                {

                    Chunk.Add(wordDictList[wordindex]);
                    counter++;
                    wordindex++;
                }
                ListOfChunks.Add(Chunk);
            }

            return ListOfChunks;
        }
        
    }
}
