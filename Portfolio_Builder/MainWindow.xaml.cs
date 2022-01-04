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

namespace Portfolio_Builder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            List<Market> items = new List<Market>();
            items.Add(new Market() { Title = "Template 1", SubSectors = { "Energie", "Landwirtschaft", "Metalle" }, TimeFrames = {"1M","2M","3M","6M","1Y","3Y","5Y" }});
            items.Add(new Market() { Title = "Template 2", SubSectors = { "Energie", "Landwirtschaft", "Metalle" }, TimeFrames = {"1M","2M","3M","6M","1Y","3Y","5Y" }});
            items.Add(new Market() { Title = "Template 3", SubSectors = { "Energie", "Landwirtschaft", "Metalle" }, TimeFrames = {"1M","2M","3M","6M","1Y","3Y","5Y" }});

            Indices.ItemsSource = items;
            Stocks.ItemsSource = items;
        }
    }

    public class Market
    {
        public string Title { get; set; }
        public List<string> SubSectors { get; set; }
        public string Chart { get; set; }
        public List<string> TimeFrames { get; set; }
        public string Symbols { get; set; }
        public string OpenAll { get; set; }

        public Market()
        {
            Title = "";
            SubSectors = new List<string>();
            Chart = "CHART";
            TimeFrames = new List<string>();
            Symbols = "Ticker List";
            OpenAll = "Alle Symbole öffnen";

        }
    }
}
