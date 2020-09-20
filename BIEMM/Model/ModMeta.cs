using BIEMM.Enums;

namespace BIEMM.Model
{
    public class ModMeta
    {
        public ModTypes ModType { get; set; }
        public string ModName { get; set; }

        public ModMeta()
        {
            ModType = ModTypes.None;
            ModName = "???";
        }

        public ModMeta(ModTypes modType, string modName)
        {
            ModType = modType;
            ModName = modName;
        }
    }
}