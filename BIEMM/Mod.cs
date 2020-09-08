using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace BIEMM
{
    public class Mod : INotifyPropertyChanged
    {
        private bool _isEnabled;

        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                _isEnabled = value;
                OnPropertyChanged();
            }
        }

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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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