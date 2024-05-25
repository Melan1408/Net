namespace Calculator
{
    public class Calculate
    {
        public static Dictionary<string, Func<double, double, double>> Operators = new Dictionary<string, Func<double, double, double>>
        {
           { "+", (a, b) => a + b },
           { "-", (a, b) => a - b },
           { "/", (a, b) => a / b },
           { "*", (a, b) => a * b },
        };

        static Calculate()
        {
            InputBuffer = "0";
            OutputBuffer = string.Empty;
            signOperator = string.Empty;
            firstNumValue = 0;
            secondNumValue = 0;

            decimalSeparator = Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);
        }

        private static string _inputBuffer;
        private static string _outputBuffer;

        private static double firstNumValue;
        private static double secondNumValue;
        private static string signOperator;

        private static char decimalSeparator;

        private static bool doesTheCalculationWas;
        public static event EventHandler InputBufferChanged;
        public static event EventHandler OutputBufferChanged;

        public static string InputBuffer
        {
            get
            {
                return _inputBuffer;
            }
            private set
            {
                _inputBuffer = value;
                OnInputBufferChanged(EventArgs.Empty);
            }
        }
        public static string OutputBuffer
        {
            get
            {
                return _outputBuffer;
            }
            private set
            {
                _outputBuffer = value;
                OnOutputBufferChanged(EventArgs.Empty);
            }
        }

        public static void EnterNumber(int num)
        {
            if(InputBuffer.Length < 12)
            {
                if (InputBuffer == "0" || doesTheCalculationWas || InputBuffer.Any(Char.IsLetter))
                {
                    InputBuffer = num.ToString();
                }
                else InputBuffer += num.ToString();
            }
   
            if (doesTheCalculationWas)
            {
                OutputBuffer = string.Empty;
                doesTheCalculationWas = false;
            }
        }

        public static void EnterSign(string sign)
        {
            if (signOperator.Length != 0) CalculateResult();

            OutputBuffer = InputBuffer;
            firstNumValue = double.Parse(InputBuffer);
            OutputBuffer += sign;
            signOperator = sign;
            InputBuffer = "0";
            if (doesTheCalculationWas)
            {
                doesTheCalculationWas = false;
            }
        }

        public static void EnterDot()
        {
            if (!InputBuffer.Contains(decimalSeparator.ToString()))
            {
                InputBuffer += decimalSeparator;
            }
        }

        public static void EraseLast()
        {
            if (InputBuffer.Length > 0)
            {
                InputBuffer = InputBuffer.Remove(InputBuffer.Length - 1);
                if (InputBuffer.Length == 0) InputBuffer = "0";
            }
        }
        public static void ClearEntry()
        {
            InputBuffer = "0";
        }
        public static void Clear()
        {
            OutputBuffer = string.Empty;
            InputBuffer = "0";
        }

        public static void CalculateResult()
        {
            try
            {
                secondNumValue = double.Parse(InputBuffer);
                InputBuffer = Operators[signOperator](firstNumValue, secondNumValue).ToString();
                doesTheCalculationWas = true;
                signOperator = string.Empty;
            }
            catch (Exception ex)
            {
                InvokeError();
            }

        }

        public static void Equal()
        {
            OutputBuffer += InputBuffer;
            OutputBuffer += "=";
            CalculateResult();
        }

        private static void InvokeError()
        {
            OutputBuffer = string.Empty;
            signOperator = string.Empty;
            InputBuffer = "Error";
        }
        protected static void OnInputBufferChanged(EventArgs e) => InputBufferChanged?.Invoke(null, e);
        protected static void OnOutputBufferChanged(EventArgs e) => OutputBufferChanged?.Invoke(null, e);
    }
}
