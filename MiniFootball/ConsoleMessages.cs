using MiniFootballLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniFootballUI
{
    class ConsoleMessages
    {
        internal static void WelcomeMessage()
        {
            Console.WriteLine("Welcome to the Mini Football game!");
        }

        internal static void StartMessage()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Please press Enter to start the game");
            Console.ReadLine();
        }

        internal static void EndMessage(Player player1, Player player2)
        {
            Console.WriteLine($"Game over! {player1.Name}: {player1.Goals} goals; {player2.Name}: {player2.Goals} goals");

            Player winner = player1;
            if (player1.Goals == player2.Goals)
            {
                Console.WriteLine($"Game is draw");
            }
            else 
            {
                if (player1.Goals < player2.Goals)
                {
                    winner = player2;
                } else
                {
                    winner = player1;
                }
                Console.WriteLine($"Congratulations, {winner.Name}! You");
            }
            
        }

    }
}
