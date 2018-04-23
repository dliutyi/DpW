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
        List<List<double>> m_matrix;
        int m_hierarchyCount;
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
                m_hierarchyCount = setupDialog.Hierarchy[0];
                m_matrix = new List<List<double>>(m_hierarchyCount);
                for(int i = 0; i < m_hierarchyCount; ++i)
                {
                    m_matrix.Add(new List<double>(m_hierarchyCount));
                    for (int j = 0; j < m_hierarchyCount; ++j)
                    {
                        m_matrix[i].Add(0);
                    }
                    m_matrix[i][i] = 1;
                }

                Grid[] grVarients = new Grid[2] { spFramework.Children[1] as Grid, spFramework.Children[3] as Grid };
                
                foreach(var grVarient in grVarients)
                {
                    grVarient.Children.Clear();
                    grVarient.ColumnDefinitions.Clear();
                    for (int i = 0; i < m_hierarchyCount; ++i)
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
                for (int i = 0; i < m_hierarchyCount; ++i)
                {
                    RowDefinition newRow = new RowDefinition();
                    ColumnDefinition newCol = new ColumnDefinition();

                    grMatrix.RowDefinitions.Add(newRow);
                    grMatrix.ColumnDefinitions.Add(newCol);

                    for(int j = 0; j < m_hierarchyCount; ++j)
                    {
                        TextBox txtBox = new TextBox();
                        txtBox.Style = FindResource("acStyle") as Style;

                        Grid.SetRow(txtBox, i);
                        Grid.SetColumn(txtBox, j);

                        grMatrix.Children.Add(txtBox);
                    }
                }

                ResetFrameworkMatrix();
            }
        }

        private void ResetFrameworkMatrix()
        {
            Grid grMatrix = spFramework.Children[5] as Grid;

            for (int i = 0; i < m_hierarchyCount; ++i)
            {
                TextBox txtBox = grMatrix.Children[i * m_hierarchyCount + i] as TextBox;
                txtBox.Text = m_matrix[i][i].ToString();
            }
        }

        private void CalculateClick(object sender, RoutedEventArgs e)
        {
            Grid grMatrix = spFramework.Children[5] as Grid;

            for (int i = 0; i < m_hierarchyCount; ++i)
            {
                TextBox txtBox = grMatrix.Children[i] as TextBox;
                m_matrix[0][i] = double.Parse(txtBox.Text);
            }

            for(int i = 0; i < m_hierarchyCount; ++i)
            {
                for(int j = 0; j < i; ++j)
                {
                    m_matrix[i][j] = 1 / m_matrix[j][i];
                }
            }
        }
    }
}
