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
    /// Логика взаимодействия для goodEditPage.xaml
    /// </summary>
    public partial class goodEditPage : Page
    {
        private goods _currentGood = new goods();
        public goodEditPage(goods selectedGood)
        {
            InitializeComponent();
            if (selectedGood != null)
            {
                _currentGood = selectedGood;
            }
            DataContext = _currentGood;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            onlineStoreEntities.GetContext().goods.Add((goods)DataContext);
            onlineStoreEntities.GetContext().SaveChanges();
        }
    }
}
