using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Globalization;
using System.IO;

namespace TobiTube_Offline
{
    class Localization
    {
        public static int SelectedLanguage { get; private set; }
        public static string LangCode;

        public static event LanguageChangeHandler OnLanguageChange;
        public delegate void LanguageChangeHandler();

        private static Dictionary<string, List<string>> localization;

        public static void Awake()
        {
            if (localization == null)
            {
                SetRegionLanguage();
                LoadLocalization();
            }
        }
        public static void SetRegionLanguage()
        {
            SelectedLanguage = 0;
            if (CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ru") { SelectedLanguage = 1; LangCode = "ru"; }
            if (CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ua") { SelectedLanguage = 2; LangCode = "ua"; }
        }
        public static void SetLanguage(int id)
        {
            SelectedLanguage = id;
            switch (id)
            {
                case 0: LangCode = "en"; break;
                case 1: LangCode = "ru"; break;
                case 2: LangCode = "ua"; break;
                default: SelectedLanguage = 0; LangCode = "en"; break;
            }

            OnLanguageChange?.Invoke();
        }
        private static void LoadLocalization()
        {
            localization = new Dictionary<string, List<string>>();

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(Application.StartupPath + @"\" + "Localization.xml");

            foreach (XmlNode key in xmlDocument["Keys"].ChildNodes)
            {
                string keyStr = key.Attributes["Name"].Value;

                var values = new List<string>();
                foreach (XmlNode translate in key["Translates"].ChildNodes)
                    values.Add(translate.InnerText);

                localization[keyStr] = values;
            }
        }
        public static string GetTranslate(string key, int languageId = -1)
        {
            if (languageId == -1)
                languageId = SelectedLanguage;

            if (localization.ContainsKey(key))
                return localization[key][languageId];

            return key;
        }

        public static void SaveLanguage()
        {
            File.WriteAllText("lang.txt",LangCode);
        }
    }
}
