using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TobiTube_Offline
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
            textBox1.Text = File.ReadAllText(Path.Combine(Application.StartupPath, "VideosPaths.txt"));

            ThemeSystem.OnColorChange += OnThemeChanged;
            Localization.OnLanguageChange += Localization_OnLanguageChange;
            Localization_OnLanguageChange();

            this.FormClosed += delegate { ThemeSystem.OnColorChange -= OnThemeChanged; Localization.OnLanguageChange -= Localization_OnLanguageChange; };
            OnThemeChanged(ThemeSystem.CurrentTheme);
            checkBox1.Checked = ThemeSystem.IsBlack;
            checkBox2.Checked = Form1.ShowFPS;
        }

        private void Localization_OnLanguageChange()
        {
            label1.Text = Localization.GetTranslate("Settings");
            label2.Text = Localization.GetTranslate("Path to video folders (Through each line)");
            label3.Text = Localization.GetTranslate("Language") + ":";
            button2.Text = Localization.GetTranslate("Add folder");
            button1.Text = Localization.GetTranslate("Save");

            checkBox1.Text = Localization.GetTranslate("Dark Theme");
            checkBox2.Text = Localization.GetTranslate("Show FPS in Home Page");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            File.WriteAllText(Path.Combine(Application.StartupPath, "VideosPaths.txt"),textBox1.Text);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            ThemeSystem.ChangeColor(checkBox1.Checked);
        }

        void OnThemeChanged(Dictionary<string,Color> e)
        {
            label1.ForeColor = e["TextColor"];
            label2.ForeColor = e["TextColor"];
            label3.ForeColor = e["TextColor"];
            button1.ForeColor = e["TextColor"];

            checkBox1.ForeColor = e["TextColor"];
            checkBox2.ForeColor = e["TextColor"];

            checkBox3.ForeColor = e["TextColor"];
            checkBox4.ForeColor = e["TextColor"];
            checkBox5.ForeColor = e["TextColor"];

            checkBox3.FlatAppearance.MouseOverBackColor = e["VideoColor"];
            checkBox4.FlatAppearance.MouseOverBackColor = e["VideoColor"];
            checkBox5.FlatAppearance.MouseOverBackColor = e["VideoColor"];

            this.BackColor = e["BackColor"];

            panel1.BackColor = e["HeadColor"];
            button1.BackColor = e["VideoColor"];

            textBox1.BackColor = e["BlockColor"];
            textBox1.ForeColor = e["TextColor"];
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            Form1.ShowFPS = checkBox2.Checked;
        }

        private void Settings_Load(object sender, EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            Localization.SetLanguage(0);
        }
        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            Localization.SetLanguage(1);
        }
        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            Localization.SetLanguage(2);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            var process = new FolderBrowserDialog();

            if (process.ShowDialog() == DialogResult.OK) {
                textBox1.Text += (textBox1.Text.Length != 0 ? Environment.NewLine : "") + process.SelectedPath;
            }
        }
    }
}
