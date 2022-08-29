using MiniFootballLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniFootballUI
{
    // class that will work with input and outputs
    class DataProcessor
    {
        internal static Player CreatePlayer(int playerID)
        {
            Console.WriteLine($"Player {playerID}, please Enter your name: ");
            string name = Console.ReadLine();

            Player player = new Player { Name = name };

            player.Footballers = new List<Footballer>();

            return player;
        }

        internal static string AskForCommand(Player activePlayer)
        {
            string command = "";
            bool isValidCommand = false;

            List<string> availableCommands = new List<string> { "run", "kick", "pass" };

            while (isValidCommand == false)
            {
                if (GameLogic.IsAttacking(activePlayer) == true)
                {
                    Console.WriteLine($"{activePlayer.Name}, enter your next move (run/pass/kick): ");
                    command = Console.ReadLine();
                    if (availableCommands.Contains(command, StringComparer.OrdinalIgnoreCase) == true)
                    {
                        isValidCommand = true;
                    }
                }
                else
                {
                    command = "run";
                    isValidCommand = true;
                }
            }

            return command;
        }

        internal static string AskForEndCoord(Player activePlayer, string command, string startCoord, List<GridSpot> grid, int movesLeft)
        {
            bool isValidCoord = false;
            string endCoord = "";

            while (isValidCoord == false)
            {
                Console.WriteLine($"{activePlayer.Name}, choose the end coordinate for {command} (e.g. B2)");
                endCoord = Console.ReadLine();

                if (GameLogic.CoordExists(endCoord, grid) == true)
                {
                    if (command == "run" && GameLogic.IsValidRun(startCoord, endCoord, grid, movesLeft) == true)
                    {
                        isValidCoord = true;
                    }
                    else if (command == "pass" && GameLogic.IsValidPass(startCoord, endCoord, grid, activePlayer) == true)
                    {
                        isValidCoord = true;
                    }
                }
            }

            return endCoord;
        }

        internal static string AskForStartCoord(Player activePlayer, List<GridSpot> grid)
        {
            string startCoord = "";
            bool isValidCoord = false;

            while (isValidCoord == false)
            {
                Console.WriteLine($"{activePlayer.Name}, choose the coordinate with a footballer (e.g. A1): ");
                startCoord = Console.ReadLine();

                if (GameLogic.CoordExists(startCoord, grid) == true)
                {
                    GridSpot startSpot = GameLogic.GetGridSpot(grid, startCoord);
                    foreach (Footballer f in activePlayer.Footballers)
                    {
                        if (f.CurrentPosition == startSpot)
                        {
                            isValidCoord = true;
                        }
                    }
                }
            }

            return startCoord;
        }
    }
}
