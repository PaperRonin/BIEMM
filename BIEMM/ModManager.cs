using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Documents;
using BIEMM.Utils;

namespace BIEMM
{
    public static class ModManager
    {
        public static void LoadAllMods(ObservableCollection<Mod> modList)
        {
            LoadModsFromFolder(modList, PathList.ModsPath);
            LoadModsFromFolder(modList, PathList.BepPatchPath);
            LoadModsFromFolder(modList, PathList.BepModsPath);
        }

        public static void ApplyMods(ObservableCollection<Mod> modList)
        {
            foreach (Mod mod in modList)
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

        private static void LoadModsFromFolder(ObservableCollection<Mod> modList, string folderPath)
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

                bool isDuplicate = false;
                foreach (var mod in modList)
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
                    break;
                }

                if (modToAdd.IsEnabled)
                {
                    File.Create(Path.Combine(PathList.ModsPath, modToAdd.ModName));
                }

                if (modToAdd.ModType == ModTypes.Mod)
                {
                    modList.Add(modToAdd);
                }
                else
                {
                    modList.Insert(0, modToAdd);
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
                    Directory.Move(pathToFile, Path.Combine(PathList.BepModsPath, "Assembly-CSharp." + mod.ModName + ".mm.dll"));
                }
                return true;
            }

            return false;
        }

        private static bool DisableMod(Mod mod)
        {
            var pathToFile = mod.ModType == ModTypes.Mod
                ? Path.Combine(PathList.BepModsPath, mod.ModName + ".dll")
                : Path.Combine(PathList.BepPatchPath, "Assembly - CSharp." + mod.ModName + ".mm.dll");

            mod.Meta.CurrentlyEnabled = false;
            if (File.Exists(pathToFile))
            {
                Directory.Move(pathToFile, Path.Combine(PathList.ModsPath, mod.ModName + ".dll"));
                return true;
            }

            return false;
        }

    }
}