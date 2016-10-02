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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ATM atm = new ATM();
            atm.LoadBanknotes(Banknote.FaceValue.Hundred, 20);
            atm.LoadBanknotes(Banknote.FaceValue.FiveHundred, 20);
            atm.LoadBanknotes(Banknote.FaceValue.Thousand, 20);


            ATMPage atmPage = new ATMPage(atm);
            atmFrame.Navigate(atmPage);
            QueuePage queuePage = new QueuePage();
            queuePage.atmPage = atmPage;
            queuePage.atm = atm;
            queueFrame.Navigate(queuePage);
            
        }
    }
}
