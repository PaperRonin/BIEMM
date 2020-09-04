using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BIEMM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public List<Mod> ModList { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            ModList = new List<Mod>();
            ModMenu.DataContext = ModList;
            var t = new Mod(ModTypes.Mod, "Bepis");
            ModList.Add(t);
            t.IsEnabled = true;
            for (int i = 0; i < 20; i++)
            {

                ModList.Add(new Mod());
            }
            ModList.Add(new Mod(ModTypes.Patch, "SUPERMEGALONGTEXTOFDEATHTHATNOONEEVERSEEN"));
        }

        private void MainWindow_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void CloseButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MinimizeButton_OnClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
    }
}
