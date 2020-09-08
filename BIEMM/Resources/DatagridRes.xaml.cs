using System.Diagnostics;
using System.Windows;
using System.Windows.Controls.Primitives;

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