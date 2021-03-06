﻿using System.Collections.ObjectModel;
using System.IO;
using BIEMM.Enums;
using BIEMM.Model;

namespace BIEMM.Utils.ModManaging
{
    public static class ModManager
    {
        private static ObservableCollection<Mod> ModList { get; set; }
        public static bool ModToggle { get; set; }
        public static void BindModList(ObservableCollection<Mod> modList) => ModList = modList;

        public static void LoadAllMods()
        {
            Logger.InfoLog($"Loading all mods from the {PathList.BepMonomodPath}");
            LoadModsFromFolder(PathList.BepMonomodPath);
            Logger.InfoLog($"Loading all mods from the {PathList.BepPluginPath}");
            LoadModsFromFolder(PathList.BepPluginPath);
            Logger.InfoLog($"Loading all mods from the {PathList.ModsFolderPath}");
            LoadModsFromFolder(PathList.ModsFolderPath);
        }

        internal static void ToggleAllMods()
        {
            Logger.InfoLog($"Toggling all mods {ModToggle}");
            foreach (Mod mod in ModList)
            {
                mod.IsEnabled = ModToggle;
            }

            ModToggle = !ModToggle;
        }

        public static void ApplyMods()
        {

            Logger.InfoLog("Applying all selected mods");
            foreach (Mod mod in ModList)
            {
                if (mod.IsEnabled && !mod.CurrentlyEnabled)
                {
                    EnableMod(mod);
                }
                else if (!mod.IsEnabled && mod.CurrentlyEnabled)
                {
                    DisableMod(mod);
                }
            }

        }

        private static void LoadModsFromFolder(string folderPath)
        {
            foreach (var modFile in (new DirectoryInfo(folderPath)).GetFiles("*.dll"))
            {
                if (Utils.ModsBlacklist.BlacklistedMods.Contains(modFile.Name))
                {
                    Logger.InfoLog($"-{modFile.Name} is in blacklist, skipping it");
                    continue;
                }

                Mod modToAdd = LoadMod(modFile);
                modToAdd.CurrentlyEnabled = folderPath != PathList.ModsFolderPath;
                modToAdd.IsEnabled = modToAdd.CurrentlyEnabled;

                bool isDuplicate = false;
                foreach (var mod in ModList)
                {
                    //if added mod is already applied
                    if (mod.Meta.ModName == modToAdd.Meta.ModName)
                    {
                        Logger.InfoLog($"-{modToAdd.Meta.ModName} is already applied, overriding it with newer version");
                        isDuplicate = true;
                        mod.IsEnabled = true;
                        mod.CurrentlyEnabled = true;
                        EnableMod(modToAdd);
                        break;
                    }
                }

                if (isDuplicate)
                {
                    continue;
                }
                ModList.Add(modToAdd);
            }

        }

        private static Mod LoadMod(FileInfo file)
        {
            Logger.InfoLog($"-Loading {file.Name}");

            string modName = Path.GetFileNameWithoutExtension(file.Name);
            if (file.Directory.FullName == PathList.BepMonomodPath)
            {
                modName = modName.Split('.')[1];
            }
            string pathToMeta = Path.Combine(PathList.ModsFolderPath, $"{modName}.json");

            ModMeta meta = null;
            if (File.Exists(pathToMeta))
            {
                meta = MetaHandler.GetMeta(pathToMeta);
            }

            if (meta == null)
            {
                meta = new ModMeta()
                {
                    ModType = ModTypeChecker.GetModType(file),
                    ModName = modName
                };
                MetaHandler.GenerateMetaFile(meta);
            }
            var mod = new Mod()
            {
                IsEnabled = false,
                CurrentlyEnabled = false,
                Meta = meta
            };

            return mod;
        }

        private static void EnableMod(Mod mod)
        {

            string pathToFile = Path.Combine(PathList.ModsFolderPath, $"{mod.Meta.ModName}.dll");

            if (File.Exists(pathToFile))
            {
                if (mod.Meta.ModType == ModTypes.Mod)
                {
                    Logger.InfoLog($"-Moved {mod.Meta.ModName} to the BepInEx/plugins");
                    File.Move(pathToFile, Path.Combine(PathList.BepPluginPath, $"{mod.Meta.ModName}.dll"), true);
                }
                else
                {
                    Logger.InfoLog($"-Moved {mod.Meta.ModName} to the BepInEx/monomod");
                    File.Move(pathToFile, Path.Combine(PathList.BepMonomodPath,
                        $"Assembly-CSharp.{mod.Meta.ModName}.mm.dll"), true);
                }
                mod.CurrentlyEnabled = true;
            }

        }

        private static void DisableMod(Mod mod)
        {
            var pathToFile = mod.Meta.ModType == ModTypes.Patch
                    ? Path.Combine(PathList.BepMonomodPath, $"Assembly-CSharp.{mod.Meta.ModName}.mm.dll")
                    : Path.Combine(PathList.BepPluginPath, $"{mod.Meta.ModName}.dll");

            mod.CurrentlyEnabled = false;
            if (File.Exists(pathToFile))
            {
                if (mod.Meta.ModType == ModTypes.Patch)
                {
                    Logger.InfoLog($"-Moved {mod.Meta.ModName} to the {PathList.BepMonomodPath}");
                    Directory.Move(pathToFile, Path.Combine(PathList.ModsFolderPath, $"{mod.Meta.ModName}.dll"));
                }
                else
                {
                    Logger.InfoLog($"-Moved {mod.Meta.ModName} to the {PathList.BepPluginPath}");
                    Directory.Move(pathToFile, Path.Combine(PathList.ModsFolderPath, $"{mod.Meta.ModName}.dll"));
                }
            }
        }
    }
}