using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Documents;
using BIEMM.Utils;

namespace BIEMM
{
    public static class ModManager
    {
        private static ObservableCollection<Mod> ModList { get; set; }
        private static bool ModToggle { get; set; }
        public static void BindModList(ObservableCollection<Mod> modList) => ModList = modList;

        public static void LoadAllMods()
        {
            LoadModsFromFolder( PathList.ModsPath);
            LoadModsFromFolder( PathList.BepPatchPath);
            LoadModsFromFolder( PathList.BepModsPath);
        }

        internal static void ToggleAllMods()
        {
            foreach (Mod mod in ModList)
            {
                mod.IsEnabled = ModToggle;
            }

            ModToggle = !ModToggle;
        }

        public static void ApplyMods()
        {
            foreach (Mod mod in ModList)
            {
                if (mod.IsEnabled && !mod.Meta.CurrentlyEnabled)
                {
                    EnableMod(mod);
                }
                else if (!mod.IsEnabled && mod.Meta.CurrentlyEnabled)
                {
                    DisableMod(mod);
                }
            }
        }

        private static void LoadModsFromFolder(string folderPath)
        {
            foreach (var modFile in (new DirectoryInfo(folderPath)).GetFiles("*.dll"))
            {
                Mod modToAdd = LoadMod(modFile);
                modToAdd.IsEnabled = folderPath != PathList.ModsPath;
                modToAdd.Meta.CurrentlyEnabled = folderPath != PathList.ModsPath;

                if (Utils.ModsBlacklist.BlacklistedMods.Contains(modToAdd.ModName))
                {
                    continue;
                }

                if (!File.Exists(Path.Combine(PathList.ModsPath, modToAdd.ModName)) && folderPath != PathList.ModsPath)
                {
                    File.Delete(modFile.FullName);
                    continue;
                }

                bool isDuplicate = false;
                foreach (var mod in ModList)
                {
                    //if added mod is already applied
                    if (mod.ModName == modToAdd.ModName)
                    {
                        isDuplicate = true;
                        mod.IsEnabled = true;
                        mod.Meta.CurrentlyEnabled = true;
                        EnableMod(modToAdd);
                        break;
                    }
                }

                if (isDuplicate)
                {
                    continue;
                }

                if (modToAdd.ModType == ModTypes.Mod)
                {
                    ModList.Add(modToAdd);
                }
                else
                {
                    ModList.Insert(0, modToAdd);
                }
            }
        }

        private static Mod LoadMod(FileInfo file)
        {
            var mod = new Mod()
            {
                IsEnabled = false,
                ModType = Utils.ModTypeChecker.GetModType(file.FullName),
                ModName = Path.GetFileNameWithoutExtension(file.Name),
                Meta = new ModMeta(File.GetLastWriteTime(file.FullName))
            };
            if (file.Directory.FullName == PathList.BepPatchPath)
            {
                mod.ModName = mod.ModName.Split('.')[1];
            }
            return mod;
        }

        private static bool EnableMod(Mod mod)
        {
            string pathToFile = Path.Combine(PathList.ModsPath, mod.ModName);
            File.Create(pathToFile);
            pathToFile += ".dll";
            if (File.Exists(pathToFile))
            {
                if (mod.ModType == ModTypes.Mod)
                {
                    Directory.Move(pathToFile, Path.Combine(PathList.BepModsPath, mod.ModName + ".dll"));
                }
                else
                {
                    Directory.Move(pathToFile, Path.Combine(PathList.BepPatchPath, "Assembly-CSharp." + mod.ModName + ".mm.dll"));
                }
                mod.Meta.CurrentlyEnabled = true;
                return true;
            }

            return false;
        }

        private static bool DisableMod(Mod mod)
        {
            var pathToFile = mod.ModType == ModTypes.Mod
                ? Path.Combine(PathList.BepModsPath, mod.ModName + ".dll")
                : Path.Combine(PathList.BepPatchPath, "Assembly-CSharp." + mod.ModName + ".mm.dll");

            mod.Meta.CurrentlyEnabled = false;
            if (File.Exists(pathToFile))
            {
                Directory.Move(pathToFile, Path.Combine(PathList.ModsPath, mod.ModName + ".dll"));
                File.Delete(Path.Combine(PathList.ModsPath, mod.ModName));
                return true;
            }

            return false;
        }

        public static void GeneratePlaceholders(string folderPath)
        {
            foreach (var modFile in (new DirectoryInfo(folderPath)).GetFiles("*.dll"))
            {
                var modName = Path.GetFileNameWithoutExtension(modFile.Name);

                if (modFile.Directory.FullName == PathList.BepPatchPath)
                {
                    modName = modName.Split('.')[1];
                }

                if (Utils.ModsBlacklist.BlacklistedMods.Contains(modName) ||
                    File.Exists(Path.Combine(PathList.ModsPath, modFile.Name)))
                {
                    continue;
                }

                File.Create(Path.Combine(PathList.ModsPath, modName)).Close();
            }
        }
    }
}