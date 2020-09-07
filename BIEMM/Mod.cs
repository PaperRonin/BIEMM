using System;
using System.Diagnostics;

namespace BIEMM
{
    public class Mod
    {
        public bool IsEnabled { get; set; }
        public ModTypes ModType { get; set; }
        public string ModName { get; set; }
        public ModMeta Meta { get; set; }

        public Mod()
        {
            IsEnabled = false;
            ModType = ModTypes.None;
            ModName = "???";
            Meta = new ModMeta(new DateTime());
        }

        public Mod(bool isEnabled, ModTypes type, string name, DateTime lastModified)
        {
            IsEnabled = isEnabled;
            ModType = type;
            ModName = name;
            Meta.LastModified = lastModified;
        }
    }

    public class ModMeta
    {
        public ModMeta(DateTime lastModified)
        {
            LastModified = lastModified;
            CurrentlyEnabled = false;

        }
        public DateTime LastModified { get; set; }
        public bool CurrentlyEnabled { get; set; }

    }
}