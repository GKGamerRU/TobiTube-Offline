using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using TobiTube_Offline.Pages;
using TobiTube_Offline.UniControls;
using TobiTube_Offline.Configs;

namespace TobiTube_Offline
{
    class TobiTubeAPI
    {
        public const string VERSION = "1.3";

        public static List<Video> AllVideos = new List<Video>();
        public static List<FileInfo> AllVideosPath = new List<FileInfo>();

        public static List<string> Paths = new List<string>();
        public static Dictionary<Type, Page> AllPages = new Dictionary<Type, Page>();
        public static Page CurrentPage;

        public static CommandsPages commandsPages;
        public static Dictionary<string, bool> GlobalExperimentOptions = new Dictionary<string, bool>()
        {
            {"showSearchRate", false }
        };

        public static void LoadHistory()
        {
            var history = GetPage<HistoryPage>();

            foreach(var word in SearchedWorlds.words)
            {
                foreach(var video in AllVideos)
                {
                    if (video.Name == word)
                    {
                        history.Result.Add(video);
                        break;
                    }
                }
            }
        }
        public static void LoadPaths()
        {
            string[] paths = null;
            if (!File.Exists(Path.Combine(Application.StartupPath, "VideosPaths.txt")))
            {
                File.WriteAllText(Path.Combine(Application.StartupPath, "VideosPaths.txt"), "");
                return;
            }
            else
            {
                paths = File.ReadAllText(Path.Combine(Application.StartupPath, "VideosPaths.txt")).Split(new string[] { Environment.NewLine },StringSplitOptions.RemoveEmptyEntries);
            }
            Paths.AddRange(paths);
            
            foreach (var video in Paths)
            {
                AllVideosPath.AddRange(new DirectoryInfo(video).GetFiles("*.*", SearchOption.AllDirectories).Where(s => s.Name.ToLower().EndsWith(".mp4") || s.Name.ToLower().EndsWith(".avi") || s.Name.ToLower().EndsWith(".mkv") || s.Name.ToLower().EndsWith(".mpg")));
            }

            Dictionary<string,string> channels = new Dictionary<string, string>();
            for (int i = 0; i < AllVideosPath.Count; i++)
            {
                var panel = AllVideosPath[i];
                string channel = null;
                foreach(var path in Paths)
                {
                    if (panel.FullName.Contains(path))
                    {
                        if (!channels.ContainsKey(path))
                        {
                            var str = path.Split(new string[] {@"\", "/" },StringSplitOptions.RemoveEmptyEntries);
                            channel = str[str.Length - 1];
                        }
                        else
                        {
                            channel = channels[path];
                        }
                    }
                }
                Video temp = new Video(Path.GetFileNameWithoutExtension(panel.Name), panel.FullName, panel.Name, panel.LastWriteTimeUtc);
                temp.Channel = channel;
                AllVideos.Add(temp);
            }
        }
        public static async void LoadLogotype(List<Video> videos, Action OnComplete)
        {
            List<Task> asks = new List<Task>();
            foreach (var panel in videos)
            {
                asks.Add(panel.GenerateIcon());
            }
            while (!Task.WaitAll(asks.ToArray(), 10))
            {
                await Task.Delay(100);
            }
            OnComplete();
        }
        static async void GenerateBestVideos()
        {
            var home = GetPage<HomePage>();
            var video = GetPage<VideoPage>();

            List<Video> videos = new List<Video>(TobiTubeAPI.AllVideos);
            List<Video> UnlogotypedVideos = new List<Video>();

            Video temp = null;

            for (int i = 0; i < 12; i++)
            {
                if (i < 6 && i < SearchedWorlds.words.Count)
                {
                    temp = Algorithms.GetRandomVideoByName(videos, SearchedWorlds.words[i]);
                    if (temp != null)
                    {
                        UnlogotypedVideos.Add(temp);
                        video.Result.Add(temp); home.BestVideos.Add(temp); videos.Remove(temp);
                        await Task.Delay(1); continue;
                    }
                    
                }
                
                int RandomInt = new Random().Next(0, AllVideos.Count - 1);

                UnlogotypedVideos.Add(AllVideos[RandomInt]);
                video.Result.Add(AllVideos[RandomInt]);
                home.BestVideos.Add(AllVideos[RandomInt]);
                
                await Task.Delay(1);
            }
            LoadLogotype(UnlogotypedVideos,delegate {
                home.MainPage.Invalidate();
                video.MainPage.Invalidate();
            });
        }
        public static async void GenerateVideos_VideoPage(List<Video> Allvideos, string text)
        {
            var video = GetPage<VideoPage>();

            List<Video> temp = new List<Video>();
            List<Video> UnlogotypedVideos = new List<Video>();

            for (int i = 0; i < 12; i++)
            {
                if (i == 0)
                {
                    Video temp2 = Algorithms.GetRandomVideoByName(Allvideos, text, true);
                    if (temp2 != null) { video.Result[i] = temp2; Allvideos.Remove(temp2); UnlogotypedVideos.Add(temp2); }

                    temp = Algorithms.GetRandomVideoByName(Allvideos, temp2, text, 6);
                    await Task.Delay(1);
                }
                if (i > 0 && i < 6)
                {
                    if (temp != null) { video.Result[i] = temp[i - 1]; UnlogotypedVideos.Add(temp[i - 1]); }
                    await Task.Delay(1);
                }
                else if(i >= 6)
                {
                    int RandomInt = new Random().Next(0, AllVideos.Count - 1);
                    UnlogotypedVideos.Add(AllVideos[RandomInt]);
                    video.Result[i] = AllVideos[RandomInt];
                    await Task.Delay(1);
                }
            }
            LoadLogotype(UnlogotypedVideos, delegate {
                video.MainPage.Invalidate();
            });
        }
        public static async void RegenerateBestVideos()
        {
            var home = GetPage<HomePage>();

            List<Video> videos = new List<Video>(TobiTubeAPI.AllVideos);
            List<Video> UnlogotypedVideos = new List<Video>();
            Video temp = null;

            for (int i = 0; i < 12; i++)
            {
                if (i < 6 && i < SearchedWorlds.words.Count)
                {
                    temp = Algorithms.GetRandomVideoByName(videos, SearchedWorlds.words[i]);
                    if (temp != null) { home.BestVideos[i] = temp; videos.Remove(temp); UnlogotypedVideos.Add(temp); temp.RecalculateData(); temp.RepaintBox(); }
                    await Task.Delay(1);
                }
                else
                {
                    int RandomInt = new Random().Next(0, AllVideos.Count - 1);
                    UnlogotypedVideos.Add(AllVideos[RandomInt]);

                    home.BestVideos[i] = AllVideos[RandomInt];
                    home.BestVideos[i].RecalculateData();
                    home.BestVideos[i].RepaintBox();
                    await Task.Delay(1);
                }
            }

            LoadLogotype(UnlogotypedVideos, delegate {
                home.MainPage.Invalidate();
            });
        }
        public static void Init(VScrollBar scroll, PictureBox rect, VScrollBar scroll2, PictureBox rect2)
        {
            Localization.Awake();
            SettingsConfig.Load();

            PagesGenerator generator = new PagesGenerator();
            generator.SetPagesOptions(scroll, rect);
            generator.SetVideoPagesOptions(scroll2, rect2);
            foreach(var page in generator.Generate())
            {
                AllPages.Add(page.GetType(),page);
            }
            CurrentPage = GetPage<HomePage>();

            commandsPages = new CommandsPages();
            commandsPages.SetPagesOptions(scroll, rect);
            commandsPages.SetVideoPagesOptions(scroll2, rect2);
            foreach (var page in commandsPages.Generate())
            {
                AllPages.Add(page.GetType(), page);
            }
            GetPage<HelpPage>().Init();

            LoadPaths();
            SearchedWorlds.LoadWords();
            LoadHistory();
            if (AllVideosPath.Count == 0) return;
            
            GenerateBestVideos();
        }
        public static void ChangeSearch(string text, string channel = null)
        {
            var home = GetPage<HomePage>();
            var search = GetPage<SearchPage>();

            if (text == "")
            {
                CurrentPage = home;
            }
            else
            {
                search.Value = text;

                if (commandsPages.TryExecuteCommand(text, ref CurrentPage))
                    return;

                CurrentPage = search;
                search.Result = AllVideos.Where(video => Algorithms.SearchPattern(video.Path, text, video) != 0 && (video.Channel == channel || channel == null)).ToList();
                foreach (var s in search.Result)
                {
                    s.SearchRate = Algorithms.SearchPattern(s.Path, text, s);
                    s.RecalculateData();
                }
                search.Result.Sort((Video a, Video b) => b.SearchRate - a.SearchRate);
            }
        }

        public static void ChangeChannel(string text, string channel = null)
        {
            var search = GetPage<ChannelPage>();

            {
                search.Value = channel;
                CurrentPage = search;

                search.Result = AllVideos.Where(video => Algorithms.SearchPattern(video.Path, text, video) != 0 && (video.Channel == channel || channel == null)).ToList();
                foreach (var s in search.Result)
                {
                    s.SearchRate = Algorithms.SearchPattern(s.Path, text, s);
                    s.RecalculateData();
                }
                search.Result.Sort((Video a, Video b) => b.SearchRate - a.SearchRate);
            }
        }

        public static T GetPage<T>() where T : Page
        {
            return (T)AllPages[typeof(T)];
        }
    }
}
