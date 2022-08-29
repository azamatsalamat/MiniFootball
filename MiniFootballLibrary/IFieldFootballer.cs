using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniFootballLibrary
{
    interface IFieldFootballer
    {
        double ScoringProb { get; set; }
        double TacklingProb { get; set; }
    }
}
