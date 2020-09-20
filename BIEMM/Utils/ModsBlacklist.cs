using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace BIEMM.Utils
{
    public static class ModsBlacklist
    {
        public static List<string> BlacklistedMods { get; set; }

        public static void LoadBlackList(string blackListSaveFile)
        {
            Logger.InfoLog("Loading BlackList");
            string[] blackList;
            if (File.Exists(blackListSaveFile))
            {
                 blackList = File.ReadAllLines(blackListSaveFile);
            }
            else
            {
                var assembly = Assembly.GetExecutingAssembly();
                var resourceName = "BIEMM.Resources.Text.BlackList.txt";
                using Stream stream = assembly.GetManifestResourceStream(resourceName);
                using StreamReader reader = new StreamReader(stream);
                blackList = reader.ReadToEnd().Split(
                    new[] {"\r\n", "\r", "\n"},
                    StringSplitOptions.None
                );
                using var sw = new StreamWriter(new FileStream(blackListSaveFile, FileMode.Create, FileAccess.Write));
                foreach (string mod in blackList)
                {
                    sw.WriteLine(mod);
                }
            }

            BlacklistedMods = blackList.ToList();

        }
    }
}