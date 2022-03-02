using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordCrackerMaster.Helper
{
    /// <summary>
    /// Helper Class containing methods to assist with reading files
    /// </summary>
    public static class ReadHelper
    {
        /// <summary>
        /// This method reads the dictionary file located in \bin\Debug\net5.0
        /// </summary>
        /// <returns>Returns a list with every line entry from the dictionary</returns>
        public static List<string> ReadDictionary()
        {
            List<string> wordlist = new List<string>();

            using (FileStream fs = new FileStream("webster-dictionary.txt", FileMode.Open, FileAccess.Read))
            using (StreamReader dictionary = new StreamReader(fs))
            {
                while (!dictionary.EndOfStream)
                {
                    string dictionaryEntry = dictionary.ReadLine();
                    wordlist.Add(dictionaryEntry);
                }
            }

            return wordlist;
        }

        public static Dictionary<string, string> ReadPasswords()
        {
            Dictionary<string, string> credentialDictionary = new Dictionary<string, string>();

            using (FileStream fs = new FileStream("passwords.txt", FileMode.Open, FileAccess.Read))
            using (StreamReader passwords = new StreamReader(fs))
            {
                while (!passwords.EndOfStream)
                {
                    string passwordsEntry = passwords.ReadLine();
                    string[] credentials = passwordsEntry.Split(":");
                    credentialDictionary.Add(credentials[0].ToString(), credentials[1].ToString());

                }
            }
            return credentialDictionary;
        }
    }
}
