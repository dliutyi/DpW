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
using System.Windows.Shapes;

namespace DpW.AppDialogs
{
    /// <summary>
    /// Логика взаимодействия для SetupWindow.xaml
    /// </summary>
    public partial class SetupWindow : Window
    {
        private List<int> m_hierarchy = new List<int>();

        public SetupWindow()
        {
            InitializeComponent();

            stackLevels.Children.Add(NewLastChildLevel());
            UpdateLevelText();
        }

        public List<int> Hierarchy { get { return m_hierarchy; } }

        private UIElement NewLastChildLevel()
        {
            StackPanel stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Horizontal;
            stackPanel.Margin = new Thickness(0, 5, 0, 5);

            TextBlock nameBlock = new TextBlock();
            nameBlock.FontSize = 17;
            nameBlock.Width = 105;
            nameBlock.VerticalAlignment = VerticalAlignment.Center;
            nameBlock.TextAlignment = TextAlignment.Center;

            TextBox countBox = new TextBox();
            countBox.Text = "0";
            countBox.FontSize = 17;
            countBox.MaxLength = 5;
            countBox.Height = 32;
            countBox.Width = 70;
            countBox.Padding = new Thickness(7, 3, 7, 4);
            nameBlock.VerticalAlignment = VerticalAlignment.Center;

            Button buttonDelete = new Button();
            buttonDelete.Content = "x";
            buttonDelete.FontSize = 33;
            buttonDelete.BorderThickness = new Thickness(0);
            buttonDelete.FontWeight = FontWeights.Bold;
            buttonDelete.Background = Brushes.White;
            buttonDelete.Padding = new Thickness(0, -11, 0, 0);
            buttonDelete.Margin = new Thickness(25, 0, 0, 0);
            buttonDelete.Click += RemoveLevelClick;

            stackPanel.Children.Add(nameBlock);
            stackPanel.Children.Add(countBox);
            stackPanel.Children.Add(buttonDelete);

            return stackPanel;
        }

        private void UpdateLevelText()
        {
            int index = 1;
            foreach(StackPanel stackPanel in stackLevels.Children)
            {
                TextBlock nameBlock = stackPanel.Children[0] as TextBlock;
                nameBlock.Text = index + " levels";
                Button buttonDelete = stackPanel.Children[2] as Button;
                buttonDelete.Tag = index;
                ++index;
            }

            scrollLevels.ScrollToEnd();
        }

        private void CreateClick(object sender, RoutedEventArgs e)
        {
            foreach (StackPanel stackPanel in stackLevels.Children)
            {
                TextBox countBox = stackPanel.Children[1] as TextBox;
                m_hierarchy.Add(int.Parse(countBox.Text));
            }
            DialogResult = true;

            Close();
        }

        private void AddLevelClick(object sender, RoutedEventArgs e)
        {
            UIElement newChild = NewLastChildLevel();
            RowDefinition newRow = new RowDefinition();
            newRow.Height = new GridLength(40);
            stackLevels.Children.Add(newChild);

            UpdateLevelText();
        }

        private void RemoveLevelClick(object sender, RoutedEventArgs e)
        {
            if (stackLevels.Children.Count > 1)
            {
                Button buttonDelete = e.Source as Button;
                int index = Convert.ToInt32(buttonDelete.Tag) - 1;

                stackLevels.Children.RemoveAt(index);

                UpdateLevelText();
            }
        }
    }
}
