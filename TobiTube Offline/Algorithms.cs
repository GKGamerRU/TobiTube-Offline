using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TobiTube_Offline.UniControls;

namespace TobiTube_Offline
{
    public static class Algorithms
    {
        public static Video GetRandomVideoByName(string value)
        {
            return GetRandomVideoByName(TobiTubeAPI.AllVideos, value);
        }
        public static Video GetRandomVideoByName(List<Video> videos, string value, bool CheckNumber = false)
        {
            List<Video> result = new List<Video>();
            string pref = RemoveTrashSymbols(value).ToLower();
            string pref2 = GetOneNumberMore(value).ToLower();
            string[] words = pref.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = words.Length - 1; i >= 0; i--)
            {
                string name = "";
                for (int b = 0; b <= i; b++)
                {
                    name += words[b];
                }
                foreach (var video in videos)
                {
                    if (video.Path.ToLower().Contains(name))
                    {
                        result.Add(video);
                    }
                }
            }
            if (CheckNumber)
            {
                foreach (var video in videos)
                {
                    if (RemoveTrashSymbols(video.Name).ToLower().Contains(pref2)) return video;
                }
            }
            if (result.Count > 1)
            { return result[new Random().Next(0, result.Count - 1)]; }

            foreach (var video in videos)
            {
                foreach (var word in words)
                {
                    if (video.Path.ToLower().Contains(word))
                    {
                        result.Add(video);
                    }
                }
            }

            if (result.Count == 0) { return null; }
            return result[new Random().Next(0, result.Count - 1)];
        }
        public static List<Video> GetRandomVideoByName(List<Video> videos, Video current, string value, int count, bool withFirst = true)
        {
            var Result = videos.Where(video => Algorithms.SearchPattern(video.Path, value, video) != 0).ToList(); //&& video.FullName != current.FullName
            videos.Remove(current);

            foreach (var s in Result)
            {
                s.SearchRate = Algorithms.SearchPattern(s.Path, value, s);
            }
            if (Result.Count > 0) Result.Sort((Video a, Video b) => b.SearchRate - a.SearchRate);

            var output = new List<Video>();
            
            if (Result.Count != 0 && withFirst) { output.Add(Result.First()); Result.RemoveAt(0); } else { count++; }
            if (Result.Count != 0 && withFirst) { output.Add(Result.First()); Result.RemoveAt(0); } else { count++; }
            for (int i = 0; i < count - 2; i++)
            {
                if (Result.Count > 0)
                {
                    int index = new Random().Next(0, Result.Count - 1);
                    output.Add(Result[index]);
                    Result.RemoveAt(index);
                }
                else {
                    int index = new Random().Next(0, videos.Count - 1);
                    output.Add(videos[index]);
                    videos.RemoveAt(index);
                }
            }
            return output;
        }

        public const string BadSymbols = "~`!@%^&*()\"№;:?{}[],.<>'|_=+-";
        public static string GetOneNumberMore(string value)
        {
            string result = value;
            foreach (var c in BadSymbols)
            {
                result = result.Replace(c, ' ');
            }

            string preResult = "";
            for (int i = 1; i < value.Length; i++)
            {
                if (result[i - 1] == ' ' && result[i] == result[i - 1])
                {
                    //preResult += "";
                }
                else
                {//|| char.IsSymbol(result[i - 1])
                    if (char.IsLetter(result[i - 1]) && char.IsNumber(result[i]) || char.IsNumber(result[i - 1]) && char.IsLetter(result[i]) || char.IsUpper(result[i]) && char.IsLower(result[i - 1]))
                    {
                        preResult += result[i - 1] + " ";
                    }
                    else
                    {
                        preResult += result[i - 1];
                    }
                }
            }
            preResult += result[result.Length - 1];

            string sumResult = "";
            int len = 0;
            bool Summed = false;
            for (int i = 0; i < preResult.Length; i++)
            {
                if (char.IsNumber(preResult[i]))
                {
                    if (!Summed) { len++; }
                    else { sumResult += preResult[i]; }
                }
                else
                {
                    if (len == 0)
                    {
                        sumResult += preResult[i];
                    }
                    else
                    {
                        string target = "";
                        for (int x = i - len; x < i; x++)
                        {
                            target += preResult[x];
                        }
                        sumResult += (Convert.ToInt32(target) + 1).ToString();
                        sumResult += preResult[i];
                        Summed = true;
                        len = 0;
                    }
                }
            }
            return sumResult;
        }
        public static string RemoveTrashSymbols(string value)
        {
            if (value.Length == 0) return value;
            string result = value;
            foreach (var c in BadSymbols) {
                result = result.Replace(c,' ');
            }
            
            string preResult = "";
            for(int i = 1; i < value.Length; i++)
            {
                if(result[i - 1] == ' ' && result[i] == result[i - 1])
                { }
                else
                {
                    if (char.IsLetter(result[i - 1]) && char.IsNumber(result[i]) || char.IsNumber(result[i - 1]) && char.IsLetter(result[i]) || char.IsUpper(result[i]) && char.IsLower(result[i - 1]))
                    {
                        preResult += result[i - 1] + " ";
                    }
                    else
                    {
                        preResult += result[i - 1];
                    }
                }
            }
            preResult += result[result.Length - 1];
            return preResult;
        }

        public static int CompareStrings(string a, string b)
        {
            int persent = 0;
            int MaxInt = Math.Max(a.Length, b.Length);
            for (int i = 0; i < Math.Min(a.Length, b.Length); i++)
            {
                if (a[i] == b[i]) persent++;
            }

            return 100 / MaxInt * persent;
        }
        public static int SearchPattern(string videoName, string search, Video obj)
        {
            int res = 0;
            if (obj.Path.ToLower().Contains(search)) { res+= 1; }
            foreach (var s in RemoveTrashSymbols(videoName).ToLower().Split(new[] { ' ', '/', '\\' }, StringSplitOptions.RemoveEmptyEntries))
            {
                foreach (var k in RemoveTrashSymbols(search).Split(new[] { ' ', '/', '\\' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (Algorithms.CompareStrings(s, k)> 60) { res++; }
                }
            }
            return res;
        }
    }
}
