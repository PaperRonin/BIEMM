using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Resources;
using System.Text.Json.Serialization;
using BIEMM.Utils;
using Microsoft.Win32;

namespace BIEMM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public List<Mod> ModList { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            ModList = new List<Mod>();
            ModMenu.DataContext = ModList;
            //var t = new Mod(ModTypes.Mod, "Bepis");
            //ModList.Add(t);
            //t.IsEnabled = true;
            //for (int i = 0; i < 20; i++)
            //{

            //    ModList.Add(new Mod());
            //}
            //ModList.Add(new Mod(ModTypes.Patch, "SUPERMEGALONGTEXTOFDEATHTHATNOONEEVERSEEN"));

            //Debug.WriteLine("-------------------------------");
            //Debug.WriteLine(CheckType(@"C:\Users\Миша\Desktop\Mods\Beastmaster.dll"));
            //Debug.WriteLine(CheckType(@"C:\Users\Миша\Desktop\Mods\PublicityStunt.dll"));
        }



        private void MainWindow_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void CloseButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MinimizeButton_OnClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            string AppDirPath = Directory.GetParent(Assembly.GetEntryAssembly().Location).FullName;
            string exePathSaveFile = Path.GetFullPath(Path.Combine(AppDirPath, "exePath.txt"));

            if (File.Exists(exePathSaveFile))
            {
                if (File.Exists(File.ReadAllText(exePathSaveFile)))
                {
                    PathList.GeneratePaths(File.ReadAllText(exePathSaveFile));
                    ModManager.LoadAllMods(ModList);
                    return;
                }
                MessageBox.Show("Couldn't find .exe file, make sure that path is correct",
                    "BIEMM",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("Select game .exe file",
                    "BIEMM",
                    MessageBoxButton.OK,
                    MessageBoxImage.Exclamation);
            }

            string fileName = "";
            OpenFileDialog selectedFile = new OpenFileDialog
            {
                Title = "Select .exe file",
                Filter = "Image Files|*.EXE;"
            };

            if ((bool)selectedFile.ShowDialog())
            {
                fileName = selectedFile.FileName;
            }

            if (fileName != "")
            {
                File.WriteAllText(@"exePath.txt", fileName);
                PathList.GeneratePaths(fileName);
                ModManager.LoadAllMods(ModList);
            }
        }

        private void OpenModsButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                Arguments = Utils.PathList.ModsPath,
                FileName = "explorer.exe"
            });
        }

        private void ReloadListButton_Click(object sender, RoutedEventArgs e)
        {
            ModList.Clear();
            ModManager.LoadAllMods(ModList);
        }

        private void ApplyModsButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RunGameButton_Click(object sender, RoutedEventArgs e)
        {

            //TODO
            try
            {
                Process p = new Process();
                //read game id and launch it through steam
                var appId = Path.Combine(Directory.GetParent(Utils.PathList.ExePath).FullName, "appid.txt");
                p.StartInfo.FileName = "steam://rungameid/" + File.ReadAllText(appId);
                p.Start();
            }
            catch (Exception exception)
            {
                MessageBoxResult msgBox = MessageBox.Show("Couldn't run the game through steam, run game locally?",
                    "BIEMM",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Error);
                if (msgBox == MessageBoxResult.Yes)
                {
                    Process.Start(Utils.PathList.ExePath);
                }
            }
        }
    }
}
