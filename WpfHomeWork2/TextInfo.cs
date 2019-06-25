using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfHomeWork2
{
    public class TextInfo : INotifyPropertyChanged
    {
        FormattedText formattedText;
        Stack<double> steps = new Stack<double>();
        public double StartFont { get; set; }
        private Rect rectExpressionLeft;
        private Rect rectExpressionRight;
        public Rect RectExpressionLeft
        {
            get { return rectExpressionLeft; }
            set
            {
                rectExpressionLeft = value;
                OnPropertyChanged("RectExpression");
            }
        }
        public Rect RectExpressionRight
        {
            get { return rectExpressionRight; }
            set
            {
                rectExpressionRight = value;
                OnPropertyChanged("RectExpression");
            }
        }

        public Rect RectExpressionLef { get; internal set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private FormattedText GetFormattedText(TextBox textBox)
        {         
            FormattedText formattedText = new FormattedText(textBox.Text, CultureInfo.CurrentCulture,
               textBox.FlowDirection, new Typeface(textBox.FontFamily, textBox.FontStyle,
               textBox.FontWeight, textBox.FontStretch), textBox.FontSize, textBox.Foreground);
            formattedText.TextAlignment = textBox.TextAlignment;          
            return formattedText;
        }
        private void DecreaseTextSize(TextBox textBox)
        {
            double step = 1, i;
            bool isChange = false;                                 
            for (i = textBox.FontSize - step; i >= step; i -= step)
            {
                if (formattedText.Width <= textBox.ActualWidth)
                    break;              
                formattedText.SetFontSize(i);
                isChange = true;
            }
            if (isChange)
            {
                steps.Push(i);
                textBox.FontSize = i;
            }
        }
        private void IncreaseTextSize(TextBox textBox)
        {
            if (textBox.Text.Length > 0)
            {
                double step = 1, i;               
                for (i = textBox.FontSize + step; i <= StartFont; i += step)
                {
                    textBox.FontSize = i;
                    formattedText.SetFontSize(i);
                    if (formattedText.Width >= textBox.ActualWidth)
                        break;
                }
            }
        }

        public void AdaptTextSize(TextBox textBox)
        {
            formattedText = GetFormattedText(textBox);
            if (formattedText.Width < textBox.ActualWidth)
                IncreaseTextSize(textBox);
            else
                DecreaseTextSize(textBox);
        }
    }
}