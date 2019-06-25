using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;

namespace WpfHomeWork2
{
    public class MyCalculator : INotifyPropertyChanged
    {
        bool IsNotInteger, mathOperation, begin = true, fromDial;
        double result, lastNumber;
        string lastMathOperation;
        char point;
        int numbersCount;
        private string number;
        private string expression;       
        public string Number
        {
            get { return number; }
            set
            {
                number = value;
                OnPropertyChanged("Number");
            }
        }
        public string Expression
        {
            get { return expression; }
            set
            {
                expression = value;
                OnPropertyChanged("Expression");
            }
        }        
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public MyCalculator(string point, int numbersCount)
        {
            this.point = char.Parse(point);
            this.numbersCount = numbersCount;
        }

        public MyCalculator()
        {
            Number = "0";
            point = ',';
        }

        public void CleanEntry()
        {
            Number = "0";
            IsNotInteger = false;
            begin = true;
            result = 0;
        }

        public void AllClean()
        {
            Number = "0";
            Expression = "";
            IsNotInteger = mathOperation = false;
            begin = true;
            result = lastNumber = 0;
            lastMathOperation = "";
        }

        private int CountNumber(string text)
        {
            int counter = 0;
            for (int i = 0; i < text.Length; i++)
                if (char.IsDigit(text[i]))
                    counter++;
            return counter;
        }
        private bool IsAddSpace(string text)
        {
            for (int i = 0; i < text.Length; i++)
                if (text[i] == point && i <= 3)
                    return false;
            return true;
        }
        private string PasteSpace(string text)
        {
            if (IsAddSpace(text))
            {
                int count = 1, startPos = -1, i = -1;
                startPos = IsNotInteger ? text.IndexOf(point) - 1 : startPos = text.Length - 1;
                for (i = startPos; i >= 0; i--)
                    if (i != 0)
                        if (count++ % 3 == 0)
                        {
                            if (char.IsDigit(text[i - 1]))
                            {
                                text = text.Insert(i, " ");
                                count = 1;
                            }
                        }
            }           
            return text;
        }

        private string DeleteSpaces(string text)
        {
            for (int i = text.Length - 1; i >= 0; i--)
                if (text[i] == ' ')
                    text = text.Remove(i, 1);
            return text;
        }

        public void AddNumber(string newNumber)
        {
            string temp = Number;
            if (mathOperation)
            {
                mathOperation = IsNotInteger = false;             
                temp = "";
            }
            if (CountNumber(temp) < 16)
            {
                if (!IsNotInteger && temp.Length > 3)
                    temp = DeleteSpaces(temp);
                if (temp == "0")
                    temp = "";
                temp += newNumber;
                if (!IsNotInteger)
                    if (temp.Length > 3)
                        temp = PasteSpace(temp);
                fromDial = true;                
            }
            Number = temp;
        }

        public void MathematicOperation(string lastMathOperation)
        {
            string temp = Number;          
            if (string.IsNullOrEmpty(this.lastMathOperation))
               this.lastMathOperation = lastMathOperation;           
            if (mathOperation && !string.IsNullOrEmpty(Expression))
            {
                Expression = Expression.Remove(expression.Length - 1, 1);
                Expression = Expression.Insert(expression.Length, lastMathOperation);
            }
            else
            {
                if (!string.IsNullOrEmpty(temp))
                {
                    Expression += " " + temp + " " + lastMathOperation;
                    lastNumber = double.Parse(temp);
                    if (fromDial)
                        PerformMathOperation(this.lastMathOperation);
                    if (!begin)
                        temp = result.ToString();
                    if (temp.Length > 3)
                        temp = PasteSpace(temp);
                    Number = temp;
                }              
            }
            this.lastMathOperation = lastMathOperation;
            mathOperation = true;     
            begin = fromDial = false;         
        }

        public void PerformMathOperation(string mathSymbol)
        {
            switch (mathSymbol)
            {
                case "/":
                    result = begin ? lastNumber : result / lastNumber;
                    break;
                case "*":
                    result = begin ? 1 : result;
                    result *= lastNumber;
                    break;
                case "-":
                    result -= lastNumber;
                    if (begin)
                        result *= -1;
                    break;
                case "+":
                    result += lastNumber;
                    break;
                default:
                    break;
            }           
        }

        public void Equally()
        {
            string temp = Number;
            if (fromDial)
                lastNumber = double.Parse(temp);
            PerformMathOperation(lastMathOperation);
            temp = result.ToString();
            if (temp.Length > 3)
                temp = PasteSpace(temp);
            begin = fromDial = false;
            mathOperation = true;
            Expression = "";
            Number = temp;
        }

        public void DeleteItem()
        {
            string temp = Number;
            if (temp.Length > 0)
            {
                if (temp[temp.Length - 1] == point)
                    IsNotInteger = false;
                temp = DeleteSpaces(temp);
                temp = temp.Remove(temp.Length - 1, 1);
                if (temp.Length > 3)
                    temp = PasteSpace(temp);
                if (string.IsNullOrEmpty(temp))
                    temp = "0";
                     Number = temp;
            }
        }

        public void ChangeSign()
        {
            try
            {
                string temp = Number;
                temp = (double.Parse(temp) * (-1.0)).ToString();
                if (!temp.Contains(point))
                    IsNotInteger = false;
                if (temp.Length > 3)
                    temp = PasteSpace(temp);
                mathOperation = true;
                Number = temp;                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }          
        }

        public void AddPoint()
        {
            string temp = Number;
            if (!temp.Contains(point))
            {
                IsNotInteger = true;
                temp += point;
            }
            else
            {
                if (temp.IndexOf(point) == temp.Length - 1)
                {
                    IsNotInteger = false;
                    temp = temp.Remove(temp.Length - 1, 1);
                }
            }
            Number = temp;
        }       
    }
}
