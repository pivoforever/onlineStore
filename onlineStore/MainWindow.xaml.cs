using System;
using System.Collections.Generic;
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

namespace onlineStore
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Manager.allFrame = myFrame;
            Manager.allFrame.Navigate(new loginPage());
            //myFrame.Navigate(new loginPage());
        }

        private void Button_back(object sender, RoutedEventArgs e)
        {
            Manager.allFrame.Navigate(new loginPage());
        }

        private void Button_charts(object sender, RoutedEventArgs e)
        {
            Manager.allFrame.Navigate(new dashboard());
        }
    }
}
