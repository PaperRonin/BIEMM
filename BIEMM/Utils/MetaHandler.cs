using System;
using System.IO;
using Newtonsoft.Json;

namespace BIEMM.Utils
{
    public class MetaHandler
    {
        public static ModMeta GetMeta(string metaFile)
        {
            string metaData = File.ReadAllText(metaFile);
            return JsonConvert.DeserializeObject<ModMeta>(metaData);
        }

        public static void GenerateMetaFile(ModMeta meta)
        {
            try
            {
                Logger.InfoLog($"-Creating meta for {meta.ModName}");
                string modMeta = JsonConvert.SerializeObject(meta, Formatting.Indented);
                File.WriteAllText(Path.Combine(PathList.ModsFolderPath, meta.ModName + ".json"), modMeta);
            }
            catch (Exception exception)
            {
                Logger.ErrorLog(exception.Message, exception.StackTrace, exception.Source);
                throw;
            }
        }

    }
}