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
using System.Windows.Forms.DataVisualization.Charting;

namespace onlineStore
{
    /// <summary>
    /// Логика взаимодействия для dashboard.xaml
    /// </summary>
    public partial class dashboard : Page
    {
        onlineStoreEntities _context = onlineStoreEntities.GetContext();
        public dashboard()
        {
            InitializeComponent();
            ChartOrders.ChartAreas.Add(new ChartArea("Main"));

            var currentSeries = new Series("Payments")
            {
                IsValueShownAsLabel = true,
            };
            ChartOrders.Series.Add(currentSeries);

            ComboUsers.ItemsSource= _context.users.ToList();
            ComboChartTypes.ItemsSource=Enum.GetValues(typeof(SeriesChartType));
        }

        private void UpdateChart(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
