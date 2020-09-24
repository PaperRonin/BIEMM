using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using BIEMM.Utils;

namespace BIEMM.View
{
    public partial class AboutWindow : Window
    {
        public AboutWindow()
        {
            InitializeComponent();
        }

        private void AboutWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            Logger.InfoLog("Loading text for Changelog TextBox");
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "BIEMM.Resources.Text.Changelog.txt";
            Stream stream = assembly.GetManifestResourceStream(resourceName);
            StreamReader reader = new StreamReader(stream);
            ChangelogTextBox.Text = reader.ReadToEnd();

            Logger.InfoLog("Loading text for About TextBox");
            resourceName = "BIEMM.Resources.Text.About.txt";
            stream = assembly.GetManifestResourceStream(resourceName);
            reader = new StreamReader(stream);
            AboutTextBox.Text = reader.ReadToEnd();
            //to get current version of the app
            System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
            AboutTextBox.Text += " " + fvi.FileVersion;

            Logger.InfoLog("Loading text for Tips TextBox");
            resourceName = "BIEMM.Resources.Text.Tips.txt";
            stream = assembly.GetManifestResourceStream(resourceName);
            reader = new StreamReader(stream);
            TipsTextBox.Text = reader.ReadToEnd();

            stream.Dispose();
            reader.Dispose();
        }

        private void Header_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        private void MinimizeButton_OnClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void CloseButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
