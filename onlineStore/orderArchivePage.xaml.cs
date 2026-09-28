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
    /// Логика взаимодействия для orderArchivePage.xaml
    /// </summary>
    public partial class orderArchivePage : Page
    {
        public orderArchivePage()
        {
            InitializeComponent();
            var userOrders=from row in onlineStoreEntities.GetContext().orders where row.user_ID == SessionManager.CurrentUserId select row;
            orderArchiveDataGrid.ItemsSource= userOrders.ToList();
        }
    }
}
