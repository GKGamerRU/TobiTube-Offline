using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TobiTube_Offline
{
    public static class SearchedWorlds
    {
        public static List<string> words = new List<string>();
        public const string FileName = "SearchedWords.txt";

        public static void LoadWords()
        {
            if (File.Exists(Path.Combine(Application.StartupPath, FileName)))
            {
                string temp = File.ReadAllText(Path.Combine(Application.StartupPath, FileName));
                words.AddRange(temp.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries));
            }

            for (int i = words.Count; i < 12; i++)
            {
                words.Add("[DO_NOT_DELETE]");
            }
        }
        public static void SaveWords()
        {
            string temp = "";
            foreach (var text in words)
            {
                if (temp != "") temp += Environment.NewLine;
                temp += text;
            }
            File.WriteAllText(Path.Combine(Application.StartupPath, FileName), temp);
        }
        public static void AddWord(string value)
        {
            bool targeted = false;
            for (int i = 0; i < 12; i++)
            {
                if (value == words[i])
                {
                    targeted = true;
                }
            }
            if (!targeted && value != "") words.Insert(0, value);
        }
    }
}
