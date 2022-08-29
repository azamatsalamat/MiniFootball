using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniFootballLibrary
{
    public class Defender : Footballer, IFieldFootballer
    {
        public Defender()
        {
            ScoringProb = 0.33;
            TacklingProb = 0.67;
            Icon = 'D';
        }
    }
}
