using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniFootballLibrary
{
    public class Footballer : IFootballer
    {
        public GridSpot CurrentPosition { get; set; }
        public bool HasBall { get; set; } = false;
        public char Icon { get; set; }
        public double ScoringProb { get; set; }
        public double TacklingProb { get; set; }
    }
}
