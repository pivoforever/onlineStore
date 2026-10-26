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
            var allOrders= _context.orders.ToList();
            var allUsers=_context.users.ToList().OrderBy(p => p.name).ToList();

            var application = new Excel.Application();
            application.SheetsInNewWorkbook = allUsers.Count();

            Excel.Workbook workbook = application.Workbooks.Add(Type.Missing);


            for (int i = 0; i < allUsers.Count(); i++)
            {
                int startRowIndex = 1;

                Excel.Worksheet worksheet = application.Worksheets.Item[i + 1];
                worksheet.Name = allUsers[i].name;

                worksheet.Cells[1][startRowIndex]="Дата заказа";
                worksheet.Cells[2][startRowIndex] = "Статус заказа";

                startRowIndex++;

                var usersOrders = allUsers[i].orders.OrderBy(p => p.date).GroupBy(p => p.statuses).OrderBy(p => p.Key.name);

                foreach (var groupOrder in usersOrders)

                {
                    Excel.Range headerRange = worksheet.Range[worksheet.Cells[1][startRowIndex], worksheet.Cells[2][startRowIndex]];
                    headerRange.Merge();
                    headerRange.Value = groupOrder.Key.name;
                    headerRange.HorizontalAlignment=Excel.XlHAlign.xlHAlignCenter;
                    headerRange.Font.Italic= true;

                    startRowIndex++;

                    foreach (var order in groupOrder)
                    {
                        worksheet.Cells[1][startRowIndex] = order.date.ToString("dd.MM.yyyy");
                        worksheet.Cells[2][startRowIndex] = order.statuses.name;

                        startRowIndex++;
                    }
                    worksheet.Cells[1][startRowIndex] = "ИТОГО:";
                    worksheet.Cells[1][startRowIndex].HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                    worksheet.Cells[1][startRowIndex].Font.Bold= true;
                    worksheet.Cells[2][startRowIndex] = groupOrder.Count();

                    startRowIndex++;
                }
            }
            application.Visible = true;
        }
    }
}
