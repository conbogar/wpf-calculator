using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace COMP123_Assignment04_Calculator
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        string input = string.Empty;
        string operand1 = string.Empty;
        string operand2 = string.Empty;
        string result = string.Empty;
        string? operation;

        private void AppendDigit(string digit)
        {
            input += digit;
            textBlock1.Text = input;
        }
        private void ZeroClick(object sender, EventArgs e) => AppendDigit("0");
        private void OneClick(object sender, EventArgs e) => AppendDigit("1");
        private void TwoClick(object sender, EventArgs e) => AppendDigit("2");
        private void ThreeClick(object sender, EventArgs e) => AppendDigit("3");
        private void FourClick(object sender, EventArgs e) => AppendDigit("4");
        private void FiveClick(object sender, EventArgs e) => AppendDigit("5");
        private void SixClick(object sender, EventArgs e) => AppendDigit("6");
        private void SevenClick(object sender, EventArgs e) => AppendDigit("7");
        private void EightClick(object sender, EventArgs e) => AppendDigit("8");
        private void NineClick(object sender, EventArgs e) => AppendDigit("9");
        private void DecimalClick(object sender, EventArgs e)
        {
            if (!input.Contains("."))
                AppendDigit(".");
        }
        private void AppendOperator(string op)
        {
            if (!string.IsNullOrEmpty(input))
            {
                operand1 = input;
                operation = op;
                input = string.Empty;
                textBlock1.Text = operand1 + " " + op;
            }
            else if (!string.IsNullOrEmpty(operand1))
            {
                operation = op;
                textBlock1.Text = operand1 + " " + op;
            }
        }

        private void Backspace()
        {
            if (!string.IsNullOrEmpty(input))
            {
                input = input.Substring(0, input.Length - 1);
                textBlock1.Text = input.Length > 0 ? input : "0";
            }
            else if (!string.IsNullOrEmpty(operation))
            {
                operation = null;
                textBlock1.Text = operand1.Length > 0 ? operand1 : "0";
            }
        }

        private void Window_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Text)) return;
            char c = e.Text[0];

            if (char.IsDigit(c))
            {
                AppendDigit(c.ToString());
                e.Handled = true;
                return;
            }

            switch (c)
            {
                case '.':
                    DecimalClick(sender, e); e.Handled = true; break;
                case '+':
                    AddClick(sender, e); e.Handled = true; break;
                case '-':
                    SubtractClick(sender, e); e.Handled = true; break;
                case '*':
                case 'x':
                case 'X':
                    MultiplyClick(sender, e); e.Handled = true; break;
                case '/':
                    DivideClick(sender, e); e.Handled = true; break;
                case '=':
                    EqualsClick(sender, e); e.Handled = true; break;
                case 'c':
                case 'C':
                    ClearClick(sender, e); e.Handled = true; break;
            }
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    EqualsClick(sender, e); e.Handled = true; break;

                case Key.Back:
                    Backspace(); e.Handled = true; break;

                case Key.Escape:
                case Key.Delete:
                    ClearClick(sender, e); e.Handled = true; break;

                case Key.Add:
                    AddClick(sender, e); e.Handled = true; break;
                case Key.Subtract:
                    SubtractClick(sender, e); e.Handled = true; break;
                case Key.Multiply:
                    MultiplyClick(sender, e); e.Handled = true; break;
                case Key.Divide:
                    DivideClick(sender, e); e.Handled = true; break;
                case Key.Decimal:
                    DecimalClick(sender, e); e.Handled = true; break;
            }
        }
        private void DivideClick(object sender, EventArgs e) => AppendOperator("÷");
        private void MultiplyClick(object sender, EventArgs e) => AppendOperator("×");
        private void AddClick(object sender, EventArgs e) => AppendOperator("+");
        private void SubtractClick(object sender, EventArgs e) => AppendOperator("-");
        private void ClearClick(object sender, EventArgs e) => ClearInputs();
        private void ClearInputs()
        {
            input = string.Empty;
            operand1 = string.Empty;
            operand2 = string.Empty;
            operation = null;
            textBlock1.Text = "";
        }
        private void EqualsClick(object sender, EventArgs e)
        {
            operand2 = input;

            if (string.IsNullOrEmpty(operation) || string.IsNullOrEmpty(input))
            {
                return;
            }

            try
            {
                double num1 = Double.Parse(operand1);
                double num2 = Double.Parse(operand2);
                if (operation == null)
                {
                    MessageBox.Show("No operation selected.", "Error");
                    ClearInputs();
                    return;
                }
                switch (operation)
                {
                    case "+":
                        result = (num1 + num2).ToString();
                        textBlock1.Text = result;
                        break;

                    case "-":
                        result = (num1 - num2).ToString();
                        textBlock1.Text = result;
                        break;

                    case "×":
                        result = (num1 * num2).ToString();
                        textBlock1.Text = result;
                        break;

                    case "÷":
                        if (num2 == 0)
                        {
                            MessageBox.Show("Division by zero", "Error");
                            ClearInputs();
                            return;
                        }
                        else
                        {
                            result = (num1 / num2).ToString();
                            textBlock1.Text = result;
                        }
                        break;
                }
                input = result;
                operand1 = string.Empty;
                operand2 = string.Empty;
                operation = null;
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid number format", "Input Error");
                ClearInputs();
            }
        }
    }
}