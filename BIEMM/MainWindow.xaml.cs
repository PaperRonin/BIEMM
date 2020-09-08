using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Resources;
using System.Text.Json.Serialization;
using System.Windows.Controls.Primitives;
using BIEMM.Utils;
using Microsoft.Win32;

namespace BIEMM
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
                File.WriteAllText(@"exePath.txt", fileName);
                PathList.GeneratePaths(fileName);

                ModManager.GeneratePlaceholders(PathList.BepPatchPath);
                ModManager.GeneratePlaceholders(PathList.BepModsPath);
                File.Create(Path.Combine(PathList.ModsPath, "1 Deleting files here will delete the corresponding mod")).Close();

                ModManager.LoadAllMods();
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
            ModManager.LoadAllMods();
        }

        private void ApplyModsButton_Click(object sender, RoutedEventArgs e)
        {
            ModManager.ApplyMods();
        }
        private void RunGameButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
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

        private void Header_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

    }
}
