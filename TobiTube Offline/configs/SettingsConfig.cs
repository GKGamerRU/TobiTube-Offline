using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace TobiTube_Offline.Configs
{
    [Serializable]
    public class SettingsConfig
    {
        private const string FileName = "AppSettings.json";
        private static SettingsConfig settings;

        public SettingsConfig()
        {
            IsBlack = true;
        }
        
        public bool IsBlack
        {
            get
            {
                return ThemeSystem.IsBlack;
            }
            set
            {
                ThemeSystem.ChangeColor(value);
            }
        }
        public int SelectedLanguage
        {
            get
            {
                return Localization.SelectedLanguage;
            }
            set
            {
                Localization.SetLanguage(value);
            }
        }

        public static void Load()
        {
            if (File.Exists(FileName))
            {
                var serializationSettings = new JsonSerializerSettings();
                serializationSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                serializationSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
                serializationSettings.TypeNameHandling = TypeNameHandling.Auto;
                serializationSettings.MissingMemberHandling = MissingMemberHandling.Ignore;

                SettingsConfig map = JsonConvert.DeserializeObject<SettingsConfig>(File.ReadAllText(FileName), serializationSettings);
                settings = map;
            }
            else
            {
                settings = new SettingsConfig();
            }
        }
        public static void Save()
        {
            var serializationSettings = new JsonSerializerSettings();
            serializationSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            serializationSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            serializationSettings.TypeNameHandling = TypeNameHandling.Auto;
            string map = JsonConvert.SerializeObject(settings, serializationSettings);
            
            if (File.Exists(FileName))
            { File.Delete(FileName); }

            try
            {
                File.WriteAllText(FileName, map);
            }
            catch  { }
        }
    }
}
