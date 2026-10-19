using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace onlineStore
{
    /// <summary>
    /// Логика взаимодействия для dashboard.xaml
    /// </summary>
    public partial class dashboard : System.Windows.Controls.Page
    {
        onlineStoreEntities _context = onlineStoreEntities.GetContext();
        public dashboard()
        {
            InitializeComponent();
            ChartOrders.ChartAreas.Add(new ChartArea("Main"));

            var currentSeries = new Series("Statuses")
            {
                IsValueShownAsLabel = true,
            };
            ChartOrders.Series.Add(currentSeries);

            ComboUsers.ItemsSource= _context.users.ToList();
            ComboChartTypes.ItemsSource=Enum.GetValues(typeof(SeriesChartType));
        }


        private void UpdateChart(object sender, SelectionChangedEventArgs e)
        {
            if (ComboChartTypes.SelectedItem is SeriesChartType currentType && ComboUsers.SelectedItem is users currentUser)
            {
                Series currentSeries = ChartOrders.Series.FirstOrDefault();
                currentSeries.ChartType = currentType;
                currentSeries.Points.Clear();

                var statusList = _context.statuses.ToList();
                //currentSeries.Points.AddXY(1, 1);
                //currentSeries.Points.AddXY(2, 2);
                //currentSeries.Points.AddXY(3, 3);
                //currentSeries.Points.AddXY(4, 4);
                //currentSeries.Points.AddXY(5, 5);
                foreach (var status in statusList)
                {
                    currentSeries.Points.AddXY(status.name,
                        _context.orders.ToList().Where(o => o.users == currentUser
                        && o.statuses == status).Count());
                }
            }

        }
    }
}
