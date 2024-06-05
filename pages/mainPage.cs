using Dashboard;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace material
{
    public partial class MainPage : MaterialForm

    {
        private readonly MarketSalesEntities _context = new MarketSalesEntities();
        public MainPage()
        {


            InitializeComponent();
            LoadCards();
            Chart1();
            Chart2();
            Chart3();
            Chart4();
            Chart5();


            this.WindowState = FormWindowState.Maximized;
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
        }
        private void LoadCards()
        {
            lbxTotalSample.Text = ConvertToMillions(TotalSampleCard()).ToString();
            lbxTotalBrand.Text = TotalBrandCard().ToString();
            lbxTotalSales.Text = TotalSalesCard().ToString();
            lbxMaxLineNetSales.Text = GetMonthName(MaxLineNetSales()).ToUpper();
        }
        private double TotalSampleCard()
        {
            var total = _context.SalesData.Count();

            return total;
        }
        private int TotalBrandCard()
        {
            var total = _context.SalesData.Select(x => x.Brand).Distinct().Count();

            return total;
        }
        private int TotalSalesCard()
        {
            var total = _context.SalesData.Select(x => x.Salesman).Distinct().Count();

            return total;
        }
        private int MaxLineNetSales()
        {
            var result = _context.SalesData
                .Where(x => x.StartDate.HasValue)  // Ensure StartDate is not null
                .GroupBy(x => x.StartDate.Value.Month)
                .Select(y => new
                {
                    Month = y.Key,
                    NetSales = y.Sum(z => z.LineNetTotal)
                })
                .OrderByDescending(x => x.NetSales)
                .FirstOrDefault();

            // Handle null result from FirstOrDefault
            if (result != null)
            {
                return result.Month;
            }
            else
            {
                throw new InvalidOperationException("No data available.");
            }
        }
        private string GetMonthName(int month)
        {

            return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
        }
        private void Chart1()
        {

            var regionSales = _context.SalesData
                .GroupBy(s => s.Region)
                .Select(g => new
                {
                    Region = g.Key,
                    Sales = g.Sum(s => s.LineNetTotal)
                })
                .OrderByDescending(g => g.Sales)
                .ToList();

            SeriesCollection series = new SeriesCollection();


            foreach (var item in regionSales)
            {
                series.Add(new PieSeries
                {
                    Title = item.Region,
                    Values = new ChartValues<ObservableValue> { new ObservableValue((double)item.Sales) },
                    DataLabels = true
                });
            }

            ChartOne.Series = series;

            ChartOne.DefaultLegend = null;
            ChartOne.LegendLocation = LegendLocation.Bottom;
            ChartOne.DefaultLegend = new DefaultLegend { Foreground = System.Windows.Media.Brushes.White };
        }
        private void Chart2()
        {

            var regionSalesInMonths = _context.SalesData
                .Where(x => x.StartDate.HasValue)
                .GroupBy(x => x.Region)
                .Select(g => new
                {
                    Region = g.Key,
                    Sales = g.GroupBy(x => x.StartDate.Value.Month)
                        .Select(m => new
                        {
                            Month = m.Key,
                            Sales = m.Sum(x => x.LineNetTotal)
                        })
                        .ToList()
                })
                .ToList(); // Ensure the query is executed

            SeriesCollection series = new SeriesCollection();
            var monthNames = new[] { "Ocak", "Şubat", "Mart", "Nisan", "Mayıs", "Haziran", "Temmuz", "Ağustos", "Eylül", "Ekim", "Kasım", "Aralık" };

            // Create a StackedColumnSeries for each region
            foreach (var regionSales in regionSalesInMonths)
            {
                var values = new ChartValues<double>(new double[12]); // Initialize with zeros for all months

                // Populate the sales data for the corresponding month
                foreach (var monthlySales in regionSales.Sales)
                {
                    values[monthlySales.Month - 1] = (double)monthlySales.Sales;
                }

                series.Add(new StackedColumnSeries
                {
                    Title = regionSales.Region,
                    Values = values,
                    DataLabels = false
                });
            }

            ChartTwo.Series = series; // Ensure the series is assigned to the chart

            // Configure the X-axis labels with month names
            ChartTwo.AxisX.Clear();
            ChartTwo.AxisX.Add(new Axis
            {
                Labels = monthNames
            });



        }
        private void Chart3()
        {


            var regionSales = _context.SalesData
                .GroupBy(s => s.Region)
                .Select(g => new
                {
                    Region = g.Key,
                    Sales = g.Sum(s => s.LineNetTotal)
                })
                .OrderByDescending(g => g.Sales)
                .ToList();

            SeriesCollection series = new SeriesCollection();


            foreach (var item in regionSales)
            {
                series.Add(new PieSeries
                {
                    Title = item.Region,
                    Values = new ChartValues<ObservableValue> { new ObservableValue((double)item.Sales) },
                    DataLabels = true
                });
            }

            ChartThree.Series = series;

            ChartThree.DefaultLegend = null;
            ChartThree.LegendLocation = LegendLocation.Bottom;
            ChartThree.DefaultLegend = new DefaultLegend { Foreground = System.Windows.Media.Brushes.White };


        }
        private void Chart4()
        {
            // Fetch and group sales data
            var categorySales = _context.SalesData
                .GroupBy(s => s.CategoryName1)
                .Select(g => new
                {
                    Category = g.Key,
                    Sales = g.GroupBy(x => x.StartDate.Value.Month)
                        .Select(m => new
                        {
                            Month = m.Key,
                            Sales = m.Sum(x => x.LineNetTotal)
                        })
                        .ToList()
                })
                .ToList();

            // Initialize SeriesCollection
            SeriesCollection series = new SeriesCollection();
            var monthNames = new[]
            {
    "Ocak", "Şubat", "Mart", "Nisan", "Mayıs", "Haziran",
    "Temmuz", "Ağustos", "Eylül", "Ekim", "Kasım", "Aralık"
};

            // Loop through each category to create series
            foreach (var categorySale in categorySales)
            {
                var values = new ChartValues<double>();

                // Loop through each month and add sales data
                for (int month = 1; month <= 12; month++)
                {
                    var sales = categorySale.Sales.FirstOrDefault(x => x.Month == month);
                    values.Add((double)(sales?.Sales ?? 0));
                }

                // Add new LineSeries to the series collection
                series.Add(new LineSeries
                {
                    Title = categorySale.Category,
                    Values = values,
                    PointGeometrySize = 15
                });
            }

            // Clear existing series before adding new series
            ChartFour.Series.Clear();
            ChartFour.Series = series;

            // Configure the chart's legend
            ChartFour.DefaultLegend = null;
            ChartFour.LegendLocation = LegendLocation.Bottom;
            ChartFour.DefaultLegend = new DefaultLegend { Foreground = System.Windows.Media.Brushes.White };

            // Configure the X-axis labels
            ChartFour.AxisX.Clear();
            ChartFour.AxisX.Add(new Axis
            {
                Labels = monthNames
            });


        }
        private void Chart5()
        {
            var totalSales = _context.SalesData
                .GroupBy(x => x.StartDate.Value.Month)
                .Select(g => new
                {
                    Month = g.Key,
                    Sales = g.Sum(x => x.LineNetTotal)
                })
                .OrderBy(x => x.Month)
                .ToList();

            // Initialize the series collection
            SeriesCollection series = new SeriesCollection();
            var monthNames = new[]
            {
    "Ocak", "Şubat", "Mart", "Nisan", "Mayıs", "Haziran",
    "Temmuz", "Ağustos", "Eylül", "Ekim", "Kasım", "Aralık"
};

            // Create a dictionary for quick lookup of total sales by month
            var salesDictionary = totalSales.ToDictionary(x => x.Month, x => x.Sales);

            // Initialize a list to store the sales values in order
            var salesValues = new ChartValues<double>();

            // Fill the salesValues list with total sales for each month in the correct order
            for (int month = 1; month <= 12; month++)
            {
                salesValues.Add(salesDictionary.ContainsKey(month) ? (double)salesDictionary[month] : 0.0);
            }

            // Add the total sales series to the series collection
            series.Add(new LineSeries
            {
                Title = "Total",
                Values = salesValues,
                PointGeometrySize = 15
            });

            // Configure the chart's legend and axis
            ChartFive.Series = series;
            ChartFive.DefaultLegend = null;
            ChartFive.LegendLocation = LegendLocation.Bottom;
            ChartFive.DefaultLegend = new DefaultLegend { Foreground = System.Windows.Media.Brushes.White };
            ChartFive.AxisX.Clear();
            ChartFive.AxisX.Add(new Axis
            {
                Labels = monthNames
            });


        }
        private string ConvertToMillions(double value)
        {

            double result = value / 1000000;

            return $"{result:F1} M";
        }
    }


}





