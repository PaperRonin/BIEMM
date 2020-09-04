using System.Diagnostics;

namespace BIEMM
{
    public class Mod
    {
        private bool _isEnabled;
        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                Debug.WriteLine("changed to " + value);
                _isEnabled = value;
            }
        }
        public ModTypes ModType { get; set; }
        public string ModName { get; set; }

        public Mod()
        {
            IsEnabled = false;
            ModType = ModTypes.None;
            ModName = "???";
        }

        public Mod(ModTypes type, string name)
        {
            IsEnabled = false;
            ModType = type;
            ModName = name;
        }
    }
}