using System.Windows;
using BIEMM.Utils.ModManaging;

namespace BIEMM.Resources
{
    public partial class DataGridRes : ResourceDictionary
    {
        public DataGridRes()
        {
            InitializeComponent();
        }

        private void IsEnabledHeader_Click(object sender, RoutedEventArgs routedEventArgs)
        {
            ModManager.ToggleAllMods();
        }
    }
}