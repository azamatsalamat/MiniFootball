using System;

namespace MiniFootballLibrary
{
    public class GridSpot
    {
        public char Row { get; set; }
        public int Column { get; set; }
        public bool Occupied { get; set; } = false;

        public int GetRowNumber()
        {
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            for (int i = 0; i<alphabet.Length; i++)
            {
                if (alphabet[i] == Row)
                {
                    return i;
                }
            }

            return 0;
        }
    }
}
