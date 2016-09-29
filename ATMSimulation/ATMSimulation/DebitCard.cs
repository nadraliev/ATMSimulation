using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMSimulation
{
    public class DebitCard
    {
        public int Pin { get { return pin; } }
        public double AvailableRubles { get { return availableRubles; } }

        private int pin;
        private double availableRubles;

        public DebitCard()
        {

        }
    }
}
