using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using BIEMM.Model;
using BIEMM.Utils;
using BIEMM.Utils.ModManaging;
using Microsoft.Win32;

namespace BIEMM.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Mod> ModList { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            ModList = new ObservableCollection<Mod>();
            ModMenu.DataContext = ModList;
            ModManager.BindModList(ModList);
        }
        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Utils.Updater.Run();
                string AppDirPath = Directory.GetParent(Assembly.GetEntryAssembly().Location).FullName;
                string exePathSaveFile = Path.GetFullPath(Path.Combine(AppDirPath, "exePath.txt"));
                File.Create(AppDirPath + "\\Info.log").Close();

                ModsBlacklist.LoadBlackList(Path.GetFullPath(Path.Combine(AppDirPath, "blackList.txt")));

                Logger.InfoLog("Checking if exe path is already saved");

                if (File.Exists(exePathSaveFile))
                {
                    //if save game .exe path exists
                    if (File.Exists(File.ReadAllText(exePathSaveFile)))
                    {
                        Logger.InfoLog("Found the exe path");
                        PathList.GeneratePaths(File.ReadAllText(exePathSaveFile));
                        ModManager.LoadAllMods();
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
                    Filter = "Image Files|*.EXE;",
                    InitialDirectory = @"C:\Program Files (x86)\Steam\steamapps\common\Rain World\"
                };

                if ((bool)selectedFile.ShowDialog())
                {
                    fileName = selectedFile.FileName;
                }

                if (fileName != "")
                {
                    Logger.InfoLog("exe path selected, saving it");
                    File.WriteAllText(@"exePath.txt", fileName);
                    PathList.GeneratePaths(fileName);
                    ModManager.LoadAllMods();
                }
            }
            catch (Exception exception)
            {
                Logger.ErrorLog(exception.Message, exception.StackTrace, exception.Source);
                throw;
            }
        }
        private void Header_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        private void AboutButton_OnClick(object sender, RoutedEventArgs e)
        {
            var w = new AboutWindow();
            w.Show();
        }
        private void MinimizeButton_OnClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void CloseButton_OnClick(object sender, RoutedEventArgs e)
        {
            foreach (Window window in Application.Current.Windows) window.Close();
        }

        private void OpenModsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Logger.InfoLog("Opening Mods folder");
                Process.Start(new ProcessStartInfo
                {
                    Arguments = Utils.PathList.ModsFolderPath,
                    FileName = "explorer.exe"
                });
            }
            catch (Exception exception)
            {
                Logger.ErrorLog(exception.Message, exception.StackTrace, exception.Source);
                throw;
            }
        }
        private void ReloadListButton_Click(object sender, RoutedEventArgs e)
        {
            ModList.Clear();
            ModManager.ModToggle = false;
            ModManager.LoadAllMods();
        }
        private void ApplyModsButton_Click(object sender, RoutedEventArgs e)
        {
            ModManager.ApplyMods();
            MessageBox.Show("Mods have been applied!", "BIEMM", MessageBoxButton.OK,
                MessageBoxImage.Information);
        }
        private void RunGameButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Logger.InfoLog("Starting the game");
                Process.Start(new ProcessStartInfo
                {
                    Arguments = "steam://rungameid/" + File.ReadAllText(Path.Combine(Directory.GetParent(Utils.PathList.ExePath).FullName, "appid.txt")),
                    FileName = "explorer.exe"
                });
            }
            catch (Exception)
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
