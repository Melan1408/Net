using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            string inputText = b.Content.ToString();

            switch (inputText)
            {
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                case "0":
                    Calculate.EnterNumber(Int32.Parse(inputText));
                    break;
                case ".":
                    Calculate.EnterDot();
                    break;
                case "⟵":
                    Calculate.EraseLast();
                    break;
                case "C":
                    Calculate.Clear();
                    break;
                case "CE":
                    Calculate.ClearEntry();
                    break;
                case "+":
                case "-":
                case "*":
                case "/":
                    Calculate.EnterSign(inputText);
                    break;
                case "=":
                    Calculate.Equal();
                    break;
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.D8:
                case Key.D0:
                case Key.D1:
                case Key.D2:
                case Key.D3:
                case Key.D4:
                case Key.D5:
                case Key.D6:
                case Key.D7:
                case Key.D9:
                    Calculate.EnterNumber((int)e.Key - 34);
                    break;
                case Key.NumPad0:
                case Key.NumPad1:
                case Key.NumPad2:
                case Key.NumPad3:
                case Key.NumPad4:
                case Key.NumPad5:
                case Key.NumPad6:
                case Key.NumPad7:
                case Key.NumPad8:
                case Key.NumPad9:
                    if (Keyboard.IsKeyToggled(Key.NumLock)) Calculate.EnterNumber((int)e.Key - 74);
                    break;
                case Key.OemMinus:
                    if (!Keyboard.IsKeyDown(Key.LeftShift)) Calculate.EnterSign("-"); 
                    break;
                case Key.OemPlus:
                    if (Keyboard.IsKeyDown(Key.LeftShift)) Calculate.EnterSign("+");
                    else Calculate.Equal();
                    break;
                case Key.Back:
                    Calculate.EraseLast();
                    break;
                case Key.Delete:
                    Calculate.ClearEntry();
                    break;         
                case Key.Multiply:
                    if (Keyboard.IsKeyToggled(Key.NumLock)) Calculate.EnterSign("*");
                    break;
                case Key.Divide:
                    if (Keyboard.IsKeyToggled(Key.NumLock)) Calculate.EnterSign("/");
                    break;
                case Key.Add:
                    if (Keyboard.IsKeyToggled(Key.NumLock)) Calculate.EnterSign("+");
                    break;
                case Key.Subtract:
                    if (Keyboard.IsKeyToggled(Key.NumLock)) Calculate.EnterSign("-");
                    break;
                case Key.Decimal:
                    if (Keyboard.IsKeyToggled(Key.NumLock)) Calculate.EnterDot();
                    break;
            }
        }

    }
}