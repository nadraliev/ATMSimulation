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
                availableBanknotes = UpdateAvailableBanknotes(banknotes);
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
            Console.WriteLine("new ATM created");
        }

        public void LoadBanknotes(Banknote.FaceValue face, int count)
        {
            for (int i = 0; i < count; i++)
                banknotes.Add(new Banknote(face));
            Console.WriteLine(face + " " + count + " banknotes loaded");
        }

        public void StartSession(Customer customer)
        {
            currentCustomer = customer;
            Console.WriteLine("new customer entered");
        }

        public void EndSession()
        {
            currentCustomer = null;
            Console.WriteLine("session ended");
        }

        private bool CheckPin(int enteredPin, DebitCard targetCard)
        {
            return enteredPin == targetCard.Pin;
        }

        public int RequestMoney(int rubles, ref List<Banknote> result)    //returns code of error
        {
            if (rubles > Rubles) return 1;  //not enough money
            if (rubles % (int)AvailableBanknotes.First() != 0) return 2;     //atm doesn't have requested banknotes
            List<Banknote> banknotesClone = new List<Banknote>();
            banknotesClone.AddRange(banknotes);
            List<Banknote.FaceValue> availableBanknotesClone = UpdateAvailableBanknotes(banknotesClone);
            int i = availableBanknotesClone.Count - 1;
            Banknote.FaceValue currentFace;
            while (rubles > 0)
            {
                currentFace = availableBanknotesClone[i];
                while (rubles >= (int)currentFace && availableBanknotesClone.Contains(currentFace))
                {
                    rubles -= (int)currentFace;
                    result.Add(new Banknote(currentFace));
                    RemoveBanknote(banknotesClone ,currentFace);
                    availableBanknotesClone = UpdateAvailableBanknotes(banknotesClone);
                }
                if (rubles % (int)availableBanknotesClone.First() != 0) return 2;
                i--;
            }
            return 0;   //no error
        }

        private void RemoveBanknote(List<Banknote> banknotes, Banknote.FaceValue face)
        {
            for (int i = 0; i < banknotes.Count; i++)
            {
                if (banknotes[i].Rubles.Equals(face))
                {
                    banknotes.RemoveAt(i);
                    return;
                }
            }
        }

        private List<Banknote.FaceValue> UpdateAvailableBanknotes(List<Banknote> banknotes)
        {
            List<Banknote.FaceValue> availableBanknotes = new List<Banknote.FaceValue>();
            foreach (Banknote banknote in banknotes)
            {
                if (!availableBanknotes.Contains(banknote.Rubles))
                    availableBanknotes.Add(banknote.Rubles);
            }
            availableBanknotes.Sort();  //ascending
            return availableBanknotes;
        }

        public void GiveMoney(List<Banknote> money)
        {
            foreach (Banknote b in money)
                RemoveBanknote(banknotes, b.Rubles);
            availableBanknotes = UpdateAvailableBanknotes(banknotes);
        }
    }
}
