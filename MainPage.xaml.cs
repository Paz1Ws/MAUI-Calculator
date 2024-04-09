using System.Globalization;

namespace CalculatorApp
{

    public partial class MainPage : ContentPage
    {
        public bool controllerZero = false;
        public char _operator = ' ';
        public int unique = 0;
        public List<double> values = new List<double>();
        public MainPage()
        {
            InitializeComponent();

        }
        private void Result(object sender, EventArgs e)
        {
            if (unique == 0)
            {
                switch (_operator)
                {
                    case '+':
                        Sum(sender, e);
                        break;
                    case '-':
                        Rest(sender, e);
                        break;
                    case '*':
                        controllerZero = true;
                        Mult(sender, e);
                        break;
                    case '/':
                        controllerZero = true;
                        Div(sender, e);
                        break;
                    default:
                        MainLabel.Text = SecondLabel.Text;
                        break;
                }

                values.Clear();
                MainLabel.Text = SecondLabel.Text;
                SecondLabel.Text = null;
            }
            unique++;
        }
        private void DeleteAll(object sender, EventArgs e)
        {
            unique = 0;
            MainLabel.Text = string.Empty;
            SecondLabel.Text = null;
            values.Clear();
        }
        private void Delete(object sender, EventArgs e)
        {
            if (MainLabel.Text != "0" && MainLabel.Text.Count() != 0) {
                MainLabel.Text = MainLabel.Text.Remove(MainLabel.Text.Length - 1);
            }
            _ = MainLabel.Text.Count() == 0 ? MainLabel.Text= "0" : MainLabel.Text;
        }
        private void Sum(object sender, EventArgs e)
        {
            try
            {
                double Number = double.Parse(MainLabel.Text.Replace(".", "."), CultureInfo.InvariantCulture);
                hasDecimalPoint = false;
                if (unique == 0)
                {
                    values.Add(Number);
                    unique = 1;
                }
                if (values.Count == 0 || SecondLabel.Text == null)
                {
                    SecondLabel.Text = MainLabel.Text; unique = 0;
                }

                else if (SecondLabel.Text != null && MainLabel.Text != "0")
                {
                    Number = double.Parse(SecondLabel.Text, CultureInfo.InvariantCulture);
                    Number += double.Parse(MainLabel.Text, CultureInfo.InvariantCulture);
                    SecondLabel.Text = Number.ToString();
                }
                _operator = '+';
            }
            catch
            {
                DisplayAlert("Ops", "No correct format", "Ok");
            }
            MainLabel.Text = "0";
        }

        private void Rest(object sender, EventArgs e)
        {
            double result = 0;
            try
            {
                double Number = double.Parse(MainLabel.Text.Replace(".", "."), CultureInfo.InvariantCulture);
                if (SecondLabel.Text != null)
                {
                    result = double.Parse(SecondLabel.Text.Replace(".", "."), CultureInfo.InvariantCulture) - Number;
                    SecondLabel.Text = result.ToString();


                }
                else
                    SecondLabel.Text = Number.ToString();
                hasDecimalPoint = false;
                _operator = '-';
            }
            catch
            {
                DisplayAlert("Ops", "No correct format", "Ok");
            }
            MainLabel.Text = "0";
        }

        private void Mult(object sender, EventArgs e)
        {
            double result = 0;
            try
            {
                double Number = double.Parse(MainLabel.Text.Replace(".", "."), CultureInfo.InvariantCulture);
                if (SecondLabel.Text != null && _operator == '*')
                {
                    if (controllerZero == true)
                    {
                        result = double.Parse(SecondLabel.Text.Replace(".", "."), CultureInfo.InvariantCulture) * Number;
                        SecondLabel.Text = result.ToString();
                        controllerZero = false;
                    }
                }
                else if (_operator != '*') { SecondLabel.Text = Number.ToString();
                    controllerZero = true; }
                    
                else if (MainLabel.Text != "0")
                {
                    SecondLabel.Text = MainLabel.Text;
                }

                hasDecimalPoint = false;
                _operator = '*';
            }
            catch
            {
                DisplayAlert("Ops", "No correct format", "Ok");
            }
            MainLabel.Text = "0";
        }

        private void Div(object sender, EventArgs e)
        {
            double result = 0;
            try
            {
                double Number = double.Parse(MainLabel.Text.Replace(".", "."), CultureInfo.InvariantCulture);
                if (SecondLabel.Text != null && _operator == '/')
                {
                    if (controllerZero == true)
                    {
                        result = double.Parse(SecondLabel.Text.Replace(".", "."), CultureInfo.InvariantCulture) / Number;
                        SecondLabel.Text = result.ToString();
                        controllerZero = false;
                    }
                }
                else if (_operator != '/')
                {
                    SecondLabel.Text = Number.ToString();
                    controllerZero = true;
                }

                else if (MainLabel.Text != "0")
                {
                    SecondLabel.Text = MainLabel.Text;
                }

                hasDecimalPoint = false;
                _operator = '/';
            }
            catch
            {
                DisplayAlert("Ops", "No correct format", "Ok");
            }
            MainLabel.Text = "0";
        }

        private bool hasDecimalPoint = false;

        private void asignValue(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (button.Text == "." && !hasDecimalPoint)
            {
                MainLabel.Text += ".";
                hasDecimalPoint = true;
            }
            else
            {
                if (MainLabel.Text == "0"  || unique == 1)
                {
                    MainLabel.Text = string.Empty;

                }
                if (MainLabel.Text.Length == 18)
                    DisplayAlert("Ops", "Not more available inputs", "Fine");

                if (char.IsDigit(button.Text[0]))
                {
                    MainLabel.Text += button.Text;
                }

                unique = 0;
            }
        }




    }
}
