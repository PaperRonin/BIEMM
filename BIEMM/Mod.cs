using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BIEMM
{
    public class Mod : INotifyPropertyChanged
    {
        private bool _isEnabled;

        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                _isEnabled = value;
                OnPropertyChanged();
            }
        }

        public bool CurrentlyEnabled { get; set; }

        public ModMeta Meta { get; set; }

        public Mod()
        {
            IsEnabled = false;
            Meta = new ModMeta();
            CurrentlyEnabled = false;
        }

        public Mod(bool isEnabled, ModTypes type, string name)
        {
            IsEnabled = isEnabled;
            Meta = new ModMeta(type, name);
            CurrentlyEnabled = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}