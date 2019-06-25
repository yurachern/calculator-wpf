using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfHomeWork2
{
    /// <summary>
    /// Логика взаимодействия для EscapeButton.xaml
    /// </summary>
    public partial class EscapeButton : Window
    {
        ButtonInfo buttonInfo;
        public EscapeButton()
        {
            InitializeComponent();
            buttonInfo = (ButtonInfo)(this.Resources["buttonInfo"]);
        }

        private void EscapeButton_MouseEnter(object sender, MouseEventArgs e)
        {
            buttonInfo.ChangeButtonPosition(Width, Height, escapeButton.Width, escapeButton.Height);
        }

        private void EscapeButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Wow, how nimble, and try to catch again!");
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.NumPad9)
            {
                escapeButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }            
        }
    }
}
