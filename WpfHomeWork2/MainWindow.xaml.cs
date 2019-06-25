using System.Windows;
using System.Windows.Controls;

namespace WpfHomeWork2
{   
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox.SelectedIndex != -1)
            {
                switch (comboBox.SelectedIndex)
                {
                    case 0:
                        Calculator calculator = new Calculator();
                        calculator.ShowDialog();
                        break;
                    case 1:
                        EscapeButton escapeButton = new EscapeButton();
                        escapeButton.ShowDialog();
                        break;                   
                }
            }
        }
    }
}
