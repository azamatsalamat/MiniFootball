using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniFootballLibrary
{
    public class Goalkeeper : Footballer
    {
        public Goalkeeper()
        {
            ScoringProb = 0;
            TacklingProb = 0.5;
            Icon = 'G';
        }
    }
}
