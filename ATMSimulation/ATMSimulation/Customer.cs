using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMSimulation
{
    public class Customer
    {
        public DebitCard Card { get { return card; } }

        private DebitCard card;

        public Customer()
        {
            card = new DebitCard();
        }
    }
}
