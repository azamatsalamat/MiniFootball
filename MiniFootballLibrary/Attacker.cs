using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniFootballLibrary
{
    public class Attacker : Footballer, IFieldFootballer
    {
        public Attacker()
        {
            ScoringProb = 0.67;
            TacklingProb = 0.33;
            Icon = 'A';
        }
    }
}
