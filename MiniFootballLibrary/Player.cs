using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniFootballLibrary
{
    public class Player
    {
        public string Name { get; set; }
        public List<Footballer> Footballers { get; set; }
        public int Goals { get; set; } = 0;
        public string GatesCoord { get; set; }
    }
}
