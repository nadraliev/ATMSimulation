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

namespace ATMSimulation
{
    /// <summary>
    /// Interaction logic for ATMPage.xaml
    /// </summary>
    public partial class ATMPage : Page
    {

        public ATM atm;

        public ATMPage(ATM atm)
        {
            InitializeComponent();
            this.atm = atm;
            foreach (var key in Enum.GetNames(typeof(Banknote.FaceValue)))
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Name = key.ToString();
                item.Content = key.ToString();
                comboBox.Items.Add(item);
            }
            comboBox.SelectedIndex = 0;
            UpdateMoneyLabel();
        }

        public void PostText(string input)
        {
            atmStatus.Content = input;
        }

        public void AddText(string input)
        {
            atmStatus.Content += input;
        }

        public void ClearText()
        {
            atmStatus.Content = String.Empty;
        }

        public void ResetStatus()
        {
            atmStatus.Content = "Welcome";
            atmMoney.Visibility = Visibility.Hidden;
        }

        public void GiveMoney()
        {
            atmMoney.Visibility = Visibility.Visible;
        }

        private void loadMoney_Click(object sender, RoutedEventArgs e)
        {
            int count = 0;
            Int32.TryParse(textBox.Text, out count);
            Banknote.FaceValue face = (Banknote.FaceValue)Enum.Parse(typeof(Banknote.FaceValue), ((ComboBoxItem)comboBox.SelectedItem).Content.ToString());
            if (count > 0)
            {
                atm.LoadBanknotes(face, count);
                UpdateMoneyLabel();
            }
        }

        public void UpdateMoneyLabel()
        {
            moneyLabel.Content = atm.Rubles;
        }
    }
}
