using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace WpfHomeWork2
{
    public class ButtonInfo : INotifyPropertyChanged
    {
        Random random = new Random(DateTime.Now.Millisecond);
        Point buttonPosition;

        public ButtonInfo()
        {
            ButtonPosition = new Point(100, 100);
        }

        public Point ButtonPosition
        {
            get { return buttonPosition; }
            set
            {
                buttonPosition = value;
                OnPropertyChanged("ButtonPosition");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        public void ChangeButtonPosition(double windowsWith, double windowsHeight, double buttonWith, double buttonHeight)
        {
            Point point = new Point(GetCor(windowsWith, buttonWith), GetCor(windowsHeight, buttonHeight));
            ButtonPosition = point;
        }

        private double GetCor(double par1, double par2)
        {
            double cor = 0;
            do
            {
                cor = par1 * random.NextDouble();
            } while (cor > par1 - par2);
            return cor;
        }
    }    

}
