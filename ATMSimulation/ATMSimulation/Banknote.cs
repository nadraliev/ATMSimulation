using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMSimulation
{
    public class Banknote
    {
        public int Rubles { get { return rubles; } }



        public enum FaceValue { Fifty=50, Hundred=100, FiveHundred=500, Thousand=1000, FiveThousand=5000 };

        private int rubles;

        public Banknote(FaceValue faceValue)
        {
            rubles = (int)faceValue;
        }
    }
}
