using AutoUpdaterDotNET;
using System;
using System.IO;
using System.Reflection;
using System.Windows;

namespace BIEMM.Utils
{
    public static class Updater
    {
        public static void Run()
        {
            AutoUpdater.CheckForUpdateEvent += AutoUpdaterOnCheckForUpdateEvent;
            AutoUpdater.RunUpdateAsAdmin = false;
            var currentDirectory = new DirectoryInfo(Assembly.GetEntryAssembly().Location);
            if (currentDirectory.Parent != null)
            {
                AutoUpdater.InstallationPath = currentDirectory.Parent.FullName;
            }
            AutoUpdater.Start("https://raw.githubusercontent.com/PaperRonin/BIEMM/master/BIEMM/UpdateInfo.xaml");
        }

        private static void AutoUpdaterOnCheckForUpdateEvent(UpdateInfoEventArgs args)
        {
            if (args.IsUpdateAvailable)
            {
                Logger.InfoLog("New update is available, suggesting to load it");
                MessageBoxResult dialogResult;
                if (args.Mandatory.Value)
                {
                    dialogResult =
                        MessageBox.Show(
                            $@"There is new version {args.CurrentVersion} available. You are using version {args.InstalledVersion}. This is required update. Press Ok to begin updating the application.", @"Update Available",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
                }
                else
                {
                    dialogResult =
                        MessageBox.Show(
                            $@"There is new version {args.CurrentVersion} available. You are using version {
                                    args.InstalledVersion
                                }. Do you want to update the application now?", @"Update Available",
                            MessageBoxButton.YesNo,
                            MessageBoxImage.Information);
                }

                if (dialogResult.Equals(MessageBoxResult.Yes) || dialogResult.Equals(MessageBoxResult.OK))
                {
                    try
                    {
                        Logger.InfoLog("Trying to load update");
                        if (AutoUpdater.DownloadUpdate(args))
                        {
                            foreach (Window window in Application.Current.Windows) window.Close();
                        }
                    }
                    catch (Exception exception)
                    {
                        Logger.ErrorLog(exception.Message,exception.StackTrace,exception.Source);
                    }
                }
            }
        }

    }
}