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
        #region v1

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

        //}

        #endregion
        //static int ClientsReady = 5;
        private static int ClientsReady;
        const int WordsPerChunk = 1000;  // If left constant it splits everything like that, no matter how many clients there are
        //Was it intended like that?
        public static List<List<string>> ReadDictionaryAndCreateChunks(List<string> wordDictList)
        {
            //int WordsPerChunk = wordDictList.Count / Program.readyList.Count;

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
                //if (ListOfChunks.Count == Program.readyList.Count)
                //{
                //    //if (ListOfChunks.First > ListOfChunks.Last)
                //    //{
                //    //    ListOfChunks[0].Concat(Chunk).ToList();
                //    //}

                //    ListOfChunks[ListOfChunks.Count - 1].AddRange(Chunk);
                //    return ListOfChunks;
                //}
                ListOfChunks.Add(Chunk);
                
            }

            return ListOfChunks;
        }

    }
}
