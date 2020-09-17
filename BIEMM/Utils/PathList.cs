using System;
using System.IO;

namespace BIEMM.Utils
{
    public static class PathList
    {
        public static string ExePath { get; private set; }
        public static string ModsFolderPath { get; private set; }
        public static string BepPluginPath { get; private set; }
        public static string BepMonomodPath { get; private set; }

        public static void GeneratePaths(string exePath)
        {
            try
            {
                Logger.InfoLog("Generating Paths to the mod directories");

                ExePath = exePath;

                string tempDirPath = Directory.GetParent(exePath).FullName;

                BepPluginPath = Path.GetFullPath(Path.Combine(tempDirPath, "BepInEx", "plugins"));

                BepMonomodPath = Path.GetFullPath(Path.Combine(tempDirPath, "BepInEx", "monomod"));

                ModsFolderPath = Directory.CreateDirectory(Path.GetFullPath(Path.Combine(tempDirPath, "Mods"))).FullName;

            }
            catch (Exception exception)
            {
                Logger.ErrorLog(exception.Message, exception.StackTrace, exception.Source);
                throw;
            }
        }
    }
}