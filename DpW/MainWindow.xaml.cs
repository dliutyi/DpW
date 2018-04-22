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
using DpW.AppDialogs;
using LiveCharts;
using LiveCharts.Wpf;

namespace DpW
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public SeriesCollection SeriesCollection { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<double> { 3, 5, 7, 4 }
                },
                new ColumnSeries
                {
                    Values = new ChartValues<decimal> { 5, 6, 2, 7 }
                }
            };

            DataContext = this;
        }

        private void CreateCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CloseCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CreateCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SetupWindow setupDialog = new SetupWindow();
            if(setupDialog.ShowDialog() == true)
            {
                int count = setupDialog.Hierarchy[0];
                Grid[] grVarients = new Grid[2] { spFramework.Children[1] as Grid, spFramework.Children[3] as Grid };
                
                foreach(var grVarient in grVarients)
                {
                    grVarient.Children.Clear();
                    grVarient.ColumnDefinitions.Clear();
                    for (int i = 0; i < count; ++i)
                    {
                        ColumnDefinition newCol = new ColumnDefinition();
                        grVarient.ColumnDefinitions.Add(newCol);

                        TextBox txtBox = new TextBox();
                        txtBox.Style = FindResource("acStyle") as Style;

                        Grid.SetColumn(txtBox, i);
                        grVarient.Children.Add(txtBox);
                    }
                }

                Grid grMatrix = spFramework.Children[5] as Grid;
                grMatrix.Children.Clear();
                grMatrix.RowDefinitions.Clear();
                grMatrix.ColumnDefinitions.Clear();
                for (int i = 0; i < count; ++i)
                {
                    RowDefinition newRow = new RowDefinition();
                    ColumnDefinition newCol = new ColumnDefinition();

                    grMatrix.RowDefinitions.Add(newRow);
                    grMatrix.ColumnDefinitions.Add(newCol);

                    for(int j = 0; j < count; ++j)
                    {
                        TextBox txtBox = new TextBox();
                        txtBox.Style = FindResource("acStyle") as Style;

                        Grid.SetRow(txtBox, i);
                        Grid.SetColumn(txtBox, j);

                        grMatrix.Children.Add(txtBox);
                    }
                }
            }
        }
    }
}
