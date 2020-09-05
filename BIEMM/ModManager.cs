using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Documents;

namespace BIEMM
{
    public static class ModManager
    {
        public static void LoadAllMods(List<Mod> ModList)
        {
            Mod newMod;

            foreach (var modFile in (new DirectoryInfo(Utils.PathList.ModsPath)).GetFiles("*.dll"))
            {
                newMod = new Mod(false,
                    Utils.ModTypeChecker.GetModType(modFile.FullName),
                    Path.GetFileNameWithoutExtension(modFile.Name));


                if (newMod.ModType == ModTypes.Mod)
                {
                    ModList.Add(newMod);
                }
                else
                {
                    ModList.Insert(0, newMod);
                }
            }
        }


        //Directory.Move(file.FullName, filepath + "\\TextFiles\\" + file.Name);

        private static void LoadPatches()
        {

        }

        private static void LoadPartialityMods()
        {

        }
    }
}