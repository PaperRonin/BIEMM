using System.Diagnostics;

namespace BIEMM
{
    public class Mod
    {
        public bool IsEnabled { get; set; }
        public ModTypes ModType { get; set; }
        public string ModName { get; set; }

        public Mod()
        {
            IsEnabled = false;
            ModType = ModTypes.None;
            ModName = "???";
        }

        public Mod(bool isEnabled, ModTypes type, string name)
        {
            IsEnabled = isEnabled;
            ModType = type;
            ModName = name;
        }
    }
}