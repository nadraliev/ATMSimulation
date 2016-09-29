using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMSimulation
{
    public class ATM
    {

        public List<Banknote.FaceValue> AvailableBanknotes { get
            {
                foreach (Banknote banknote in banknotes)
                {
                    if (!availableBanknotes.Contains(banknote.Rubles))
                        availableBanknotes.Add(banknote.Rubles);
                }
                availableBanknotes.Sort();  //ascending
                return availableBanknotes;
            } }
        

        private double Rubles
        {
            get
            {
                double result = 0;
                foreach (Banknote banknote in banknotes)
                    result += (int)banknote.Rubles;
                return result;
            }
        }

        private List<Banknote.FaceValue> availableBanknotes;
        private List<Banknote> banknotes;
        private Customer currentCustomer;


        public ATM()
        {
            availableBanknotes = new List<Banknote.FaceValue>();
            banknotes = new List<Banknote>();
        }

        public void StartSession(Customer customer)
        {
            currentCustomer = customer;
        }

        public void EndSession()
        {
            currentCustomer = null;
        }

        private bool CheckPin(int enteredPin, DebitCard targetCard)
        {
            return enteredPin == targetCard.Pin;
        }

        private int RequestMoney(int rubles)    //returns code of error
        {
            if (rubles > Rubles) return 1;  //not enough money
            if (rubles % (int)AvailableBanknotes.First() != 0) return 2;     //atm doesn't have requested banknotes

            return 0;   //no error
        }

        private void GiveMoney(List<Banknote> money)
        {

        }
    }
}
