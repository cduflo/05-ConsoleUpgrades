using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;



namespace ConsoleUpgrades
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

        //Define Global Variables for holding total and calculating change
        public static decimal total = 0;
        public static decimal tendered = 0;
        public static decimal change = 0;

        //Define class and properties for use in calculation
        class Money
        {
            public decimal amount { get; set; }
            public string print { get; set; }
        }

        List<Money> changeMoney = new List<Money>
                {
                new Money { amount = 100, print = "Hundred Dollar Bills: " },
                new Money { amount = 50, print = "Fifty Dollar Bills: " },
                new Money { amount = 20, print = "Twenty Dollar Bills: "},
                new Money { amount = 10, print = "Ten Dollar Bills: " },
                new Money { amount = 5, print = "Five Dollar Bills: " },
                new Money { amount = 1, print = "One Dollar Bills: " },
                new Money { amount = .25M, print = "Quarters: " },
                new Money { amount = .10M, print = "Dimes: " },
                new Money { amount = .05M, print = "Nickles: " },
                new Money { amount = .01M, print = "Pennies: " }
                };

        //Methods to capture and validate user input
        public void getItemCost()
        {
            try
            {
                decimal x = decimal.Parse(textBoxCost.Text);
                x = decimal.Round(x, 2, MidpointRounding.AwayFromZero);
                total += x;
            }
            catch (Exception)
            {
                MessageBox.Show("Error: Please enter a valid amount in this format: '15.90'");
            }
        }

        public void getTendered()
        {
            try
            {
                decimal x = decimal.Parse(textBoxTender.Text);
                x = decimal.Round(x, 2, MidpointRounding.AwayFromZero);
                tendered = x;
            }
            catch (Exception)
            {
                MessageBox.Show("Error: Please enter a valid amount in this format: '15.90'");
                tendered = 0;
            }
        }

        public void button_Click(object sender, RoutedEventArgs e)
        {
            getItemCost();
            textBlockTotal.Text = total.ToString();
            textBoxCost.Text = "";
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            //Update Tendered Variable
            getTendered();

            //Set variable for change calculation
            change = tendered - total;

            //Set basis for message:
            string message = "The customer's change is: $" + change + ".\n";

            //Set variable for reducing change amount in following loop
            decimal remaining = change;
           

            //Loop through changeMoney and add to message string
            for (var i = 0; i < changeMoney.Count; i++)
            {
                decimal y = Convert.ToInt32(Math.Floor(remaining / changeMoney[i].amount));
                if (y > 0)
                {
                    message += "\n"+(changeMoney[i].print + y);
                }
                remaining = remaining % changeMoney[i].amount;
            }

            //Print message
            if (total == 0)
            {
                MessageBox.Show("No items have been added to the total.");
                textBlockChange.Text = "";
            }
            else if (tendered == 0)
            {
                textBlockChange.Text = "";
            }
            else if (tendered < total)
            {
                MessageBox.Show("The customer has presented insufficient funds for their purchase.");
            }
            else
            {
                textBlockChange.Text = message;
            }
        }


        private void textBoxCost_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void textBoxTender_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
