using Microsoft.Office.Interop.Excel;
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
using Excel = Microsoft.Office.Interop.Excel;

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
            ChartOrders.ChartAreas.Add(new System.Windows.Forms.DataVisualization.Charting.ChartArea("Main"));

            var currentSeries = new System.Windows.Forms.DataVisualization.Charting.Series("Statuses")
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
                System.Windows.Forms.DataVisualization.Charting.Series currentSeries = ChartOrders.Series.FirstOrDefault();
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

        private void BtnExportToExcel_Click(object sender, RoutedEventArgs e)
        {
            var allUser= _context.users.ToList().OrderBy(p  => p.name).ToList();

            var application = new Excel.Application();
            application.SheetsInNewWorkbook = allUser.Count();

            Excel.Workbook workbook = application.Workbooks.Add(Type.Missing);

            int startRowIndex = 1;

            for (int i = 0; i < allUser.Count(); i++)
            {
                Excel.Worksheet worksheet = application.Worksheets.Item[i + 1];
                worksheet.Name= allUser[i].name;
            }
        }
    }
}
