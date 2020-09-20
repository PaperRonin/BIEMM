using System;
using System.IO;
using BIEMM.Model;
using Newtonsoft.Json;

namespace BIEMM.Utils.ModManaging
{
    public class MetaHandler
    {
        public static ModMeta GetMeta(string metaFile)
        {
            try
            {
                string metaData = File.ReadAllText(metaFile);
                return JsonConvert.DeserializeObject<ModMeta>(metaData);
            }
            catch (Exception exception)
            {
                Logger.ErrorLog(exception.Message, exception.StackTrace, exception.Source);
                return null;
            }
        }

        public static void GenerateMetaFile(ModMeta meta)
        {
            Logger.InfoLog($"-Creating meta for {meta.ModName}");
            string modMeta = JsonConvert.SerializeObject(meta, Formatting.Indented);
            File.WriteAllText(Path.Combine(PathList.ModsFolderPath, meta.ModName + ".json"), modMeta);
        }

    }
}