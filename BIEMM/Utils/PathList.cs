using System.IO;

namespace BIEMM.Utils
{
    public static class PathList
    {
        public static string ExePath { get; private set; }
        public static string ModsFolderPath { get; private set; }
        public static string ModsPath { get; private set; }
        public static string PatchPath { get; private set; }
        public static string BepModsPath { get; private set; }
        public static string BepPatchPath { get; private set; }

        public static void GeneratePaths(string exePath)
        {
            ExePath = exePath;

            string tempDirPath = Directory.GetParent(exePath).FullName;

            BepModsPath = Path.GetFullPath(Path.Combine(tempDirPath, "BepInEx", "plugins"));

            BepPatchPath = Path.GetFullPath(Path.Combine(tempDirPath, "BepInEx", "monomod"));

            tempDirPath = Directory.CreateDirectory(Path.GetFullPath(Path.Combine(tempDirPath, "Mods"))).FullName;

            ModsFolderPath = tempDirPath;

            File.Create(Path.Combine(ModsFolderPath, "Deleting files in these folders will delete the corresponding mod")).Close();

            ModsPath = Directory.CreateDirectory(Path.GetFullPath(Path.Combine(ModsFolderPath, "Plugins"))).FullName;

            PatchPath = Directory.CreateDirectory(Path.GetFullPath(Path.Combine(ModsFolderPath, "Patches"))).FullName;
        }
    }
}