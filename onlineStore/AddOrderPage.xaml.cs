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
    /// Логика взаимодействия для AddOrderPage.xaml
    /// </summary>
    public partial class AddOrderPage : Page
    {
        private orders_details _currentOrderDetails = new orders_details();
        private orders _currentOrder = new orders()
        {
            date = DateTime.Now,
            user_ID = SessionManager.CurrentUserId,
            status_ID = 4
        };
        public AddOrderPage()
        {
            InitializeComponent();
            DataContext = _currentOrderDetails;
            goodsComboBox.ItemsSource = onlineStoreEntities.GetContext().goods.ToList();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            onlineStoreEntities.GetContext().orders.Add(_currentOrder);
            _currentOrderDetails.order_ID = _currentOrder.ID;
            onlineStoreEntities.GetContext().orders_details.Add(_currentOrderDetails);
            onlineStoreEntities.GetContext().SaveChanges();
        }

        private void goodsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            goodsPriceLabel.Content = "Цена: " + (goodsComboBox.SelectedItem as goods).price + " руб.";
        }
    }
}
