using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfHomeWork2
{
    public partial class Calculator : Window
    {      
        MyCalculator calculator;
        TextInfo textInfo = new TextInfo();
        double DefaultFontSize;
        bool afterStart;
        Button pressedButton;
        public Calculator()
        {
            InitializeComponent();
            Height = MinHeight;
            Width = MinWidth;           
            calculator = (MyCalculator)(this.Resources["calculator"]);
            textInfo.PropertyChanged += Calculator_PropertyChanged;
            DefaultFontSize = textInfo.StartFont = tbNumber.FontSize;
            afterStart = true;
         }
        private void UpdatePositionTextInExpression()
        {
            textInfo.RectExpressionLeft = tbExpression.GetRectFromCharacterIndex(0);
            textInfo.RectExpressionRight = tbExpression.GetRectFromCharacterIndex(tbExpression.Text.Length);
        }

        private void Calculator_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            btnArrowLeft.Visibility = textInfo.RectExpressionLeft.X < 0 ? Visibility.Visible : Visibility.Hidden;
            btnArrowRight.Visibility = textInfo.RectExpressionRight.TopRight.X > Width ? Visibility.Visible : Visibility.Hidden;
        }

        private void BtnNumeric_Click(object sender, RoutedEventArgs e)
        {            
            calculator.AddNumber((sender as Button).Content.ToString());           
        }
        
        private void BtnChangeSign_Click(object sender, RoutedEventArgs e)
        {           
            calculator.ChangeSign();           
        }

        private void BtnCleanEntry_Click(object sender, RoutedEventArgs e)
        {
            calculator.CleanEntry();
            tbNumber.FontSize = DefaultFontSize;
        }

        private void BtnAddPoint_Click(object sender, RoutedEventArgs e)
        {
            calculator.AddPoint();
        }

        private void BtnAllClean_Click(object sender, RoutedEventArgs e)
        {
            calculator.AllClean();
            tbNumber.FontSize = DefaultFontSize;
        }

        private void BtnMathematicOperation_Click(object sender, RoutedEventArgs e)
        {
            calculator.MathematicOperation((sender as Button).Content.ToString());
            tbExpression.PageRight();
        }

        private void BtnDeleteItem_Click(object sender, RoutedEventArgs e)
        {
            calculator.DeleteItem();          
        }

        private void BtnEqually_Click(object sender, RoutedEventArgs e)
       {           
            calculator.Equally();            
        }

        private void TbExpression_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdatePositionTextInExpression();
        }
        private void ArrowLeft_Click(object sender, RoutedEventArgs e)
        {
            tbExpression.PageLeft();
            UpdatePositionTextInExpression();
        }

        private void ArrowRight_Click(object sender, RoutedEventArgs e)
        {
            tbExpression.PageRight();
            UpdatePositionTextInExpression();
        }
       
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {         
            UpdatePositionTextInExpression();
            textInfo.AdaptTextSize(tbNumber);
        }

        private void TbNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (afterStart)
                textInfo.AdaptTextSize(tbNumber);
        }
        private void ButtonOn(Button button)
        {
            pressedButton = button;
            if (pressedButton != null)
            {
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(pressedButton, new object[] { true });              
                pressedButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
        }
        private void ButtonOff()
        {
            if (pressedButton != null)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(pressedButton, new object[] { false });
            pressedButton = null;
        }
        private void SetNumericButtonOn(Key key)
        {
            switch (key)
            {
                case Key.D0:
                case Key.NumPad0:                   
                    ButtonOn(btn0);
                    break;
                case Key.D1:
                case Key.NumPad1:                   
                    ButtonOn(btn1);
                    break;
                case Key.D2:
                case Key.NumPad2:                 
                    ButtonOn(btn2);
                    break;
                case Key.D3:
                case Key.NumPad3:                  
                    ButtonOn(btn3);
                    break;
                case Key.D4:
                case Key.NumPad4:                 
                    ButtonOn(btn4);
                    break;
                case Key.D5:
                case Key.NumPad5:                  
                    ButtonOn(btn5);
                    break;
                case Key.D6:
                case Key.NumPad6:                   
                    ButtonOn(btn6);
                    break;
                case Key.D7:
                case Key.NumPad7:                  
                    ButtonOn(btn7);
                    break;
                case Key.D8:
                case Key.NumPad8:                    
                    ButtonOn(btn8);
                    break;
                case Key.D9:
                case Key.NumPad9:                
                    ButtonOn(btn9);
                    break;
            }
        }
        private void SetMathematicButtonON(Key key)
        {
            switch (key)
            {                                                                           
                case Key.Multiply:                  
                    ButtonOn(btnMul);
                    break;
                case Key.Divide:                  
                    ButtonOn(btnDiv);
                    break;
                case Key.Add:                  
                    ButtonOn(btnAdd);
                    break;
                case Key.Subtract:                
                    ButtonOn(btnSub);
                    break;                             
                case Key.Decimal:                 
                    ButtonOn(btnPoint);
                    break;
                case Key.Enter:                 
                    ButtonOn(btnEnter);
                    break;
            }
        }
        private void SetFinctionalButtonOn(Key key)
        {
            switch (key)
            {
                case Key.Back:
                    ButtonOn(btnDeleteItem);
                    break;
                case Key.Escape:
                    ButtonOn(btnAllClean);
                    break;
                case Key.Left:
                    ButtonOn(btnArrowLeft);
                    break;
                case Key.Right:
                    ButtonOn(btnArrowRight);
                    break;
            }
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            SetNumericButtonOn(e.Key);
            SetMathematicButtonON(e.Key);
            SetFinctionalButtonOn(e.Key);
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            ButtonOff();
        }
    }
}