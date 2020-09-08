using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace BIEMM.Utils
{
    public static class PathList
    {
        public static string ExePath { get; private set; }
        public static string ModsPath { get; private set; }
        public static string BepModsPath { get; private set; }
        public static string BepPatchPath { get; private set; }

        public static void GeneratePaths(string exePath)
        {
            ExePath = exePath;

            string tempGameDirPath = Directory.GetParent(ExePath).FullName;

            ModsPath = Directory.CreateDirectory(Path.GetFullPath(Path.Combine(tempGameDirPath, "Mods"))).FullName;

            BepModsPath = Path.GetFullPath(Path.Combine(tempGameDirPath, "BepInEx", "plugins"));

            BepPatchPath = Path.GetFullPath(Path.Combine(tempGameDirPath, "BepInEx", "monomod"));
        }
    }
}