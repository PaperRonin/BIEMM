using System;
using BIEMM.Utils;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;

namespace BIEMM
{
    public static class ModManager
    {
        private static ObservableCollection<Mod> ModList { get; set; }
        public static bool ModToggle { get; set; }
        public static void BindModList(ObservableCollection<Mod> modList) => ModList = modList;

        public static void LoadAllMods()
        {
            Logger.InfoLog($"Loading all mods from the {PathList.ModsPath}");
            LoadModsFromFolder(PathList.ModsPath);

            Logger.InfoLog($"Loading all mods from the {PathList.PatchPath}");
            LoadModsFromFolder(PathList.PatchPath);

            Logger.InfoLog($"Loading all mods from the {PathList.BepModsPath}");
            LoadModsFromFolder(PathList.BepModsPath);

            Logger.InfoLog($"Loading all mods from the {PathList.BepPatchPath}");
            LoadModsFromFolder(PathList.BepPatchPath);
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
            try
            {
                Logger.InfoLog("Applying all selected mods");
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
            catch (Exception exception)
            {
                Logger.ErrorLog(exception.Message, exception.StackTrace, exception.Source);
                throw;
            }
        }

        private static void LoadModsFromFolder(string folderPath)
        {
            try
            {
                foreach (var modFile in (new DirectoryInfo(folderPath)).GetFiles("*.dll"))
                {
                    Mod modToAdd = LoadMod(modFile);
                    modToAdd.Meta.CurrentlyEnabled = folderPath != PathList.ModsPath && folderPath != PathList.PatchPath;
                    modToAdd.IsEnabled = modToAdd.Meta.CurrentlyEnabled;

                    if (Utils.ModsBlacklist.BlacklistedMods.Contains(modToAdd.ModName))
                    {
                        Logger.InfoLog("--This mod is in blacklist, skipping it");
                        continue;
                    }

                    if (!File.Exists(Path.Combine(PathList.ModsPath, modToAdd.ModName)) && folderPath == PathList.BepModsPath ||
                        !File.Exists(Path.Combine(PathList.PatchPath, modToAdd.ModName)) && folderPath == PathList.BepPatchPath)
                    {
                        Logger.InfoLog($"-{modToAdd.ModName} doesn't have placeholder deleting it");
                        File.Delete(modFile.FullName);
                        continue;
                    }

                    bool isDuplicate = false;
                    foreach (var mod in ModList)
                    {
                        //if added mod is already applied
                        if (mod.ModName == modToAdd.ModName)
                        {
                            Logger.InfoLog($"-{modToAdd.ModName} is already applied, overriding it with newer version");
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
                    ModList.Insert(0, modToAdd);
                }
            }
            catch (Exception exception)
            {
                Logger.ErrorLog(exception.Message, exception.StackTrace, exception.Source);
                throw;
            }
        }

        private static Mod LoadMod(FileInfo file)
        {
            try
            {
                Logger.InfoLog($"-Loading {file.Name}");
                var mod = new Mod()
                {
                    IsEnabled = false,
                    ModType = Utils.ModTypeChecker.GetModType(file),
                    ModName = Path.GetFileNameWithoutExtension(file.Name),
                    Meta = new ModMeta(File.GetLastWriteTime(file.FullName))
                };
                if (file.Directory.FullName == PathList.BepPatchPath)
                {
                    mod.ModName = mod.ModName.Split('.')[1];
                }
                return mod;
            }
            catch (Exception exception)
            {
                Logger.ErrorLog(exception.Message, exception.StackTrace, exception.Source);
                throw;
            }
        }

        private static bool EnableMod(Mod mod)
        {
            try
            {
                Logger.InfoLog($"-Generating placeholder for {mod.ModName}");
                string pathToFile = mod.ModType == ModTypes.Patch ? Path.Combine(PathList.PatchPath, mod.ModName) : Path.Combine(PathList.ModsPath, mod.ModName);
                File.Create(pathToFile);
                pathToFile += ".dll";
                if (File.Exists(pathToFile))
                {
                    if (mod.ModType == ModTypes.Mod)
                    {
                        Logger.InfoLog($"-Moved {mod.ModName} to the BepInEx/plugins");
                        Directory.Move(pathToFile, Path.Combine(PathList.BepModsPath, $"{mod.ModName}.dll"));
                    }
                    else
                    {
                        Logger.InfoLog($"-Moved {mod.ModName} to the BepInEx/monomod");
                        Directory.Move(pathToFile, Path.Combine(PathList.BepPatchPath,
                            $"Assembly-CSharp.{mod.ModName}.mm.dll"));
                    }
                    mod.Meta.CurrentlyEnabled = true;
                    return true;
                }

                return false;
            }
            catch (Exception exception)
            {
                Logger.ErrorLog(exception.Message, exception.StackTrace, exception.Source);
                throw;
            }
        }

        private static bool DisableMod(Mod mod)
        {
            try
            {
                var pathToFile = mod.ModType == ModTypes.Mod
                    ? Path.Combine(PathList.BepModsPath, $"{mod.ModName}.dll")
                    : Path.Combine(PathList.BepPatchPath, $"Assembly-CSharp.{mod.ModName}.mm.dll");

                mod.Meta.CurrentlyEnabled = false;
                if (File.Exists(pathToFile))
                {
                    if (mod.ModType == ModTypes.Patch)
                    {
                        Logger.InfoLog($"-Moved {mod.ModName} to the Mods/Patches and deleting it's placeholder");
                        Directory.Move(pathToFile, Path.Combine(PathList.PatchPath, $"{mod.ModName}.dll"));
                        File.Delete(Path.Combine(PathList.PatchPath, mod.ModName));
                    }
                    else
                    {
                        Logger.InfoLog($"-Moved {mod.ModName} to the Mods/Plugins and deleting it's placeholder");
                        Directory.Move(pathToFile, Path.Combine(PathList.ModsPath, $"{mod.ModName}.dll"));
                        File.Delete(Path.Combine(PathList.ModsPath, mod.ModName));
                    }
                    return true;
                }

                return false;
            }
            catch (Exception exception)
            {
                Logger.ErrorLog(exception.Message, exception.StackTrace, exception.Source);
                throw;
            }
        }

        public static void GeneratePlaceholders(string folderPath)
        {
            try
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

                    Logger.InfoLog($"-Generating placeholder for {modName}");
                    if (folderPath == PathList.BepPatchPath)
                    {
                        File.Create(Path.Combine(PathList.PatchPath, modName)).Close();
                    }
                    else
                    {
                        File.Create(Path.Combine(PathList.ModsPath, modName)).Close();
                    }
                }
            }
            catch (Exception exception)
            {
                Logger.ErrorLog(exception.Message, exception.StackTrace, exception.Source);
                throw;
            }
        }
    }
}