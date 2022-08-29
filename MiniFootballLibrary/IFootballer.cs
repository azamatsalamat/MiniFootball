using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniFootballLibrary
{
    interface IFootballer
    {
        GridSpot CurrentPosition { get; set; }
        char Icon { get; set; }
    }
}
