using System;
using System.Collections.Generic;
using MiniFootballLibrary;

namespace MiniFootballUI
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleMessages.WelcomeMessage();

            Player player1 = DataProcessor.CreatePlayer(1);
            player1.GatesCoord = "D1";
            Player player2 = DataProcessor.CreatePlayer(2);
            player2.GatesCoord = "D12";

            List<GridSpot> grid = GameLogic.CreateGrid();
            GameLogic.CreateFootballers(player1, player2, grid, true);
            GameLogic.ShowGrid(grid, player1, player2);

            ConsoleMessages.StartMessage();

            //GameLogic.Pass(player1, player2, "D6", "D4", grid);
            //GameLogic.ShowGrid(grid, player1, player2);

            Player activePlayer = player1;
            Player previousPlayer = player2;
            Player placeHolder;

            for (int i = 0; i<20; i++)
            {
                int movesNumber = GameLogic.DiceRoll();
                System.Threading.Thread.Sleep(3000);

                for (int j = 0; j < movesNumber; j++)
                {
                    Console.WriteLine($"{activePlayer.Name}, you have {movesNumber - j} moves left");

                    string command = DataProcessor.AskForCommand(activePlayer);
                    string startCoord;
                    string endCoord;

                    bool success = true;
                    
                    if (command == "pass")
                    {
                        Footballer ballFootballer = GameLogic.GetBallPlayer(player1, player2);
                        startCoord = ballFootballer.CurrentPosition.Row + ballFootballer.CurrentPosition.Column.ToString();
                        endCoord = DataProcessor.AskForEndCoord(activePlayer, command, startCoord, grid, movesNumber - j);
                        success = GameLogic.Pass(activePlayer, previousPlayer, startCoord, endCoord, grid);
                    }
                    else if (command == "run")
                    {
                        startCoord = DataProcessor.AskForStartCoord(activePlayer, grid);
                        endCoord = DataProcessor.AskForEndCoord(activePlayer, command, startCoord, grid, movesNumber - j);
                        success = GameLogic.Run(activePlayer, previousPlayer, startCoord, endCoord, grid);
                        int runLength = GameLogic.GetPath(startCoord, endCoord, grid).Count - 1;
                        j += runLength - 1;
                    }
                    else if (command == "kick")
                    {
                        Footballer ballFootballer = GameLogic.GetBallPlayer(player1, player2);
                        startCoord = ballFootballer.CurrentPosition.Row + ballFootballer.CurrentPosition.Column.ToString();
                        success = GameLogic.Kick(activePlayer, previousPlayer, startCoord, grid);
                    }

                    GameLogic.ShowGrid(grid, player1, player2);

                    if (success == false)
                    {
                        j = movesNumber;
                    }
                }

                placeHolder = previousPlayer;
                previousPlayer = activePlayer;
                activePlayer = placeHolder;

                Console.WriteLine($"Score:\nPlayer 1 {player1.Name}: {player1.Goals}\nPlayer 2 {player2.Name}: {player2.Goals}");
            }

            ConsoleMessages.EndMessage(player1, player2);
        }
    }
}
