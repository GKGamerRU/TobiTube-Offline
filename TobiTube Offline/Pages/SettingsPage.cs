using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TobiTube_Offline.UniControls;
using Button = TobiTube_Offline.UniControls.Button;

namespace TobiTube_Offline.Pages
{
    public class SettingsPage : Page
    {
        public string SETTINGS = "Settings";
        public string LANGUAGE = "Language";

        public ContentList Paths = new ContentList("Video Paths", 600, 400);

        public Toogle IsBlack = new Toogle("isBlack", 200);
        public Toogle canShowFPS = new Toogle("Show FPS in Home page", 500);

        public Button Engilsh = new Button("English", 100), Russian = new Button("Русский", 100), Ukranian = new Button("Український", 150);
        public GradientButton AddFolder = new GradientButton("Add Folder", 200, Color.DeepSkyBlue);
        public GradientButton SavePaths = new GradientButton("Save", 600, delegate { return ThemeSystem.CurrentTheme["VideoColor"]; });
        
        public SettingsPage(VScrollBar scroll, PictureBox rect)
        {
            MainPage_ScrollBar = scroll;
            MainPage = rect;

            Localization.OnLanguageChange += Localization_OnLanguageChange;
            Localization_OnLanguageChange();

            if (File.Exists(Path.Combine(Application.StartupPath, "VideosPaths.txt")))
            {
                var text = File.ReadAllLines(Path.Combine(Application.StartupPath, "VideosPaths.txt"));
                foreach (var line in text)
                {
                    Button button = new Button(line, Paths.Scale.Width - 10 - Paths.rect.Scale.Width, 10);
                    button.OnClick += delegate { Paths.Elements.Remove(button); };
                    Paths.Elements.Add(button);
                }
            }

            IsBlack.Checked = ThemeSystem.IsBlack;
            canShowFPS.Checked = Form1.ShowFPS;

            SavePaths.OnClick += delegate
            {
                string[] lines = new string[Paths.Elements.Count];
                for (int i = 0; i < lines.Length; i++)
                {
                    lines[i] = ((Button)Paths.Elements[i]).Value;
                }
                File.WriteAllLines(Path.Combine(Application.StartupPath, "VideosPaths.txt"), lines);
            };
            AddFolder.OnClick += delegate
            {
                var process = new FolderBrowserDialog();

                if (process.ShowDialog() == DialogResult.OK)
                {
                    Button button = new Button(process.SelectedPath, Paths.Scale.Width - 10 - Paths.rect.Scale.Width, 10);
                    button.OnClick += delegate { Paths.Elements.Remove(button); };
                    Paths.Elements.Add(button);
                }
            };

            Engilsh.OnClick += delegate { Localization.SetLanguage(0); };
            Russian.OnClick += delegate { Localization.SetLanguage(1); };
            Ukranian.OnClick += delegate { Localization.SetLanguage(2); };

            IsBlack.OnClick += delegate { ThemeSystem.ChangeColor(IsBlack.Checked); };
            canShowFPS.OnClick += delegate { Form1.ShowFPS = canShowFPS.Checked; };
        }

        private void Localization_OnLanguageChange()
        {
            SETTINGS = Localization.GetTranslate("Settings");
            Paths.Title = Localization.GetTranslate("Path to video folders (Through each line)");
            Paths.ApplyControl();
            LANGUAGE = Localization.GetTranslate("Language") + ":";
            AddFolder.Value = Localization.GetTranslate("Add folder");
            AddFolder.ApplyControl();
            SavePaths.Value = Localization.GetTranslate("Save");
            SavePaths.ApplyControl();

            IsBlack.Value = Localization.GetTranslate("Dark Theme");
            canShowFPS.Value = Localization.GetTranslate("Show FPS in Home Page");

            MainPage.Invalidate();
        }

        public override void Redraw(Graphics e, Point Target, bool Click)
        {
            bool targeted = false;

            int x = 5, y = 5 - MainPage_ScrollBar.Value;
            e.DrawString(SETTINGS, new Font("Segoe UI", 18), new SolidBrush(ThemeSystem.CurrentTheme["TextColor"]), x, y);
            y += 36;
            y += 36;
            x = MainPage.Width / 2 - Paths.Scale.Width / 2;

            Paths.Click = Click;
            Paths.Target = Target;
            DrawControl(Target, Paths, e, x, y, Click, ref targeted);
            DrawControl(Target, AddFolder, e, x - 10 + Paths.Scale.Width - AddFolder.Scale.Width, y - 5, Click, ref targeted);
            y += 400;
            DrawControl(Target, SavePaths, e, x, y, Click, ref targeted);

            y += 50;
            DrawControl(Target, IsBlack, e, x, y, Click, ref targeted);
            y += 36;
            DrawControl(Target, canShowFPS, e, x, y, Click, ref targeted);

            y += 36;
            y += 36;
            e.DrawString(LANGUAGE, new Font("Segoe UI", 18), new SolidBrush(ThemeSystem.CurrentTheme["TextColor"]), x, y);
            y += 36;
            DrawControl(Target, Engilsh, e, x, y, Click, ref targeted);
            x += 105;
            DrawControl(Target, Russian, e, x, y, Click, ref targeted);
            x += 105;
            DrawControl(Target, Ukranian, e, x, y, Click, ref targeted);
            x -= 310;
            y += 50;

            ApplyResult(targeted);

            if (y + MainPage_ScrollBar.Value < MainPage.Height) { MainPage_ScrollBar.Visible = false; } else { MainPage_ScrollBar.Visible = true; }
            if (MainPage_ScrollBar.Visible) { MainPage_ScrollBar.Maximum = y + MainPage_ScrollBar.Value - MainPage.Height + 50; } else { MainPage_ScrollBar.Maximum = 0; }
        }
    }
}
