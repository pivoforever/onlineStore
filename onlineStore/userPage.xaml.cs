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
    /// Логика взаимодействия для userPage.xaml
    /// </summary>
    public partial class userPage : Page
    {
        onlineStoreEntities bd;
        public userPage()
        {
            InitializeComponent();
            bd = new onlineStoreEntities();
            userDataGrid.ItemsSource=bd.goods.ToList();
        }
    }
}
