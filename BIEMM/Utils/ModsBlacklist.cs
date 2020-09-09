using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BIEMM.Utils
{
    public static class ModsBlacklist
    {
        public static List<string> BlacklistedMods { get; set; }

        public static void LoadBlackList(string blackListSaveFile)
        {
            try
            {
                Logger.InfoLog("Loading BlackList");
                var blackList = File.ReadAllLines(blackListSaveFile);
                BlacklistedMods = blackList.ToList();
            }
            catch (Exception exception)
            {
                Logger.ErrorLog(exception.Message, exception.StackTrace, exception.Source);
                throw;
            }
        }
    }
}