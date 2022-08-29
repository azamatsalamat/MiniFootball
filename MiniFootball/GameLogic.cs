using MiniFootballLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniFootballUI
{
    class GameLogic
    {
        internal static List<GridSpot> CreateGrid()
        {
            List<GridSpot> grid = new List<GridSpot>();
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            for (int i = 0; i<7; i++)
            {
                for (int j = 1; j<13; j++)
                {
                    GridSpot spot = new GridSpot { Row = alphabet[i], Column = j };
                    grid.Add(spot);
                }
            }

            return grid;
        }

        internal static GridSpot GetGridSpot(List<GridSpot> grid, string coordinate)
        {
            GridSpot spot = grid.Where(spot => spot.Row == coordinate[0] && spot.Column == Convert.ToInt32(coordinate[1..].ToString())).ToList()[0];
            return spot;
        }

        internal static void CreateFootballers(Player player1, Player player2, List<GridSpot> grid, bool isPlayerOneActive)
        {
            player1.Footballers = new List<Footballer>();
            player2.Footballers = new List<Footballer>();

            if (isPlayerOneActive == true)
            {
                Attacker attacker1 = new Attacker() { HasBall = true };
                PlaceFootballer(attacker1, "D6", grid);
                player1.Footballers.Add(attacker1);
                Attacker attacker2 = new Attacker();
                PlaceFootballer(attacker2, "D4", grid);
                player1.Footballers.Add(attacker2);
                Defender defender1 = new Defender();
                PlaceFootballer(defender1, "C3", grid);
                player1.Footballers.Add(defender1);
                Defender defender2 = new Defender();
                PlaceFootballer(defender2, "E3", grid);
                player1.Footballers.Add(defender2);
                Goalkeeper goalkeeper1 = new Goalkeeper();
                PlaceFootballer(goalkeeper1, "D1", grid);
                player1.Footballers.Add(goalkeeper1);

                Attacker attacker3 = new Attacker();
                PlaceFootballer(attacker3, "C8", grid);
                player2.Footballers.Add(attacker3);
                Attacker attacker4 = new Attacker();
                PlaceFootballer(attacker4, "E8", grid);
                player2.Footballers.Add(attacker4);
                Defender defender3 = new Defender();
                PlaceFootballer(defender3, "C10", grid);
                player2.Footballers.Add(defender3);
                Defender defender4 = new Defender();
                PlaceFootballer(defender4, "E10", grid);
                player2.Footballers.Add(defender4);
                Goalkeeper goalkeeper2 = new Goalkeeper();
                PlaceFootballer(goalkeeper2, "D12", grid);
                player2.Footballers.Add(goalkeeper2);
            }
            else
            {
                Attacker attacker1 = new Attacker();
                PlaceFootballer(attacker1, "C5", grid);
                player1.Footballers.Add(attacker1);
                Attacker attacker2 = new Attacker();
                PlaceFootballer(attacker2, "E5", grid);
                player1.Footballers.Add(attacker2);
                Defender defender1 = new Defender();
                PlaceFootballer(defender1, "C3", grid);
                player1.Footballers.Add(defender1);
                Defender defender2 = new Defender();
                PlaceFootballer(defender2, "E3", grid);
                player1.Footballers.Add(defender2);
                Goalkeeper goalkeeper1 = new Goalkeeper();
                PlaceFootballer(goalkeeper1, "D1", grid);
                player1.Footballers.Add(goalkeeper1);

                Attacker attacker3 = new Attacker() { HasBall = true };
                PlaceFootballer(attacker3, "D7", grid);
                player2.Footballers.Add(attacker3);
                Attacker attacker4 = new Attacker();
                PlaceFootballer(attacker4, "D9", grid);
                player2.Footballers.Add(attacker4);
                Defender defender3 = new Defender();
                PlaceFootballer(defender3, "C10", grid);
                player2.Footballers.Add(defender3);
                Defender defender4 = new Defender();
                PlaceFootballer(defender4, "E10", grid);
                player2.Footballers.Add(defender4);
                Goalkeeper goalkeeper2 = new Goalkeeper();
                PlaceFootballer(goalkeeper2, "D12", grid);
                player2.Footballers.Add(goalkeeper2);
            }
            
        }

        internal static void ShowGrid(List<GridSpot> grid, Player player1, Player player2)
        {
            Console.Write(" ");
            for (int i = 1; i<10; i++)
            {
                Console.Write($"  {i}");
            }

            for (int i = 10; i<13; i++)
            {
                Console.Write($" {i}");
            }

            foreach (GridSpot spot in grid)
            {
                if (spot.Column == 1)
                {
                    Console.Write($"\n{spot.Row}  ");
                }
                
                if (spot.Occupied == true)
                {
                    foreach(Footballer footballer in player1.Footballers)
                    {
                        if (footballer.CurrentPosition == spot)
                        {
                            ConsoleColor defaultColor = Console.ForegroundColor;
                            Console.ForegroundColor = ConsoleColor.Red;
                            if (footballer.HasBall == true)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                            }
                            Console.Write($"{footballer.Icon}  ");
                            Console.ForegroundColor = defaultColor;
                        }
                    }

                    foreach (Footballer footballer in player2.Footballers)
                    {
                        if (footballer.CurrentPosition == spot)
                        {
                            ConsoleColor defaultColor = Console.ForegroundColor;
                            Console.ForegroundColor = ConsoleColor.Blue;
                            if (footballer.HasBall == true)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkBlue;
                            }
                            Console.Write($"{footballer.Icon}  ");
                            Console.ForegroundColor = defaultColor;
                        }
                    }
                }
                else
                {
                    Console.Write("*  ");
                }
            }
            Console.WriteLine();
        }

        internal static void PlaceFootballer(Footballer footballer, string position, List<GridSpot> grid)
        {
            GridSpot spot = GetGridSpot(grid, position);
            if (footballer.CurrentPosition != null)
            {
                footballer.CurrentPosition.Occupied = false;
            }
            footballer.CurrentPosition = spot;
            spot.Occupied = true;
        }

        internal static bool Pass(Player activePlayer, Player opponent, string startCoord, string endCoord, List<GridSpot> grid)
        {
            bool passSuccess = true;

            Console.WriteLine($"Making a pass from {startCoord} to {endCoord}...");
            Footballer startFootballer = GetFootballer(activePlayer, startCoord, grid);
            Footballer endFootballer = GetFootballer(activePlayer, endCoord, grid);

            startFootballer.HasBall = false;

            List<GridSpot> path = GetPath(startCoord, endCoord, grid);

            double minDistance = 100;
            Footballer oppFootballer = opponent.Footballers[0];

            foreach (Footballer footballer in opponent.Footballers)
            {
                foreach (GridSpot spot in path)
                {
                    double distance = FindDistance(footballer.CurrentPosition, spot);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        oppFootballer = footballer;
                    }
                }
            }

            if (minDistance > 3)
            {
                minDistance = 3;
            }

            int passDistance = 6;
            if (path.Count < 6)
            {
                passDistance = path.Count;
            }

            double passProb = 0.5 * (minDistance / 3) + 0.5 * (1 - passDistance / 6);

            System.Threading.Thread.Sleep(3000);

            Random rand = new Random();
            if (rand.Next(1, 101) < passProb * 100)
            {
                endFootballer.HasBall = true;
                Console.WriteLine($"Pass was successful. {endFootballer.CurrentPosition.Row}{endFootballer.CurrentPosition.Column} has the ball now.");
            }
            else
            {
                oppFootballer.HasBall = true;
                Console.WriteLine($"Pass was tackled. {oppFootballer.CurrentPosition.Row}{oppFootballer.CurrentPosition.Column} has the ball now.");
                passSuccess = false;
            }

            return passSuccess;
        }

        internal static List<GridSpot> GetPath(string startCoord, string endCoord, List<GridSpot> grid)
        {
            GridSpot startSpot = GetGridSpot(grid, startCoord);
            GridSpot endSpot = GetGridSpot(grid, endCoord);

            List<GridSpot> path = new List<GridSpot>();

            if (startSpot.GetRowNumber() == endSpot.GetRowNumber())
            {
                path = grid.Where(cell => cell.GetRowNumber() == startSpot.GetRowNumber() && cell.Column >= Compare(startSpot.Column, endSpot.Column).Item1 && cell.Column <= Compare(startSpot.Column, endSpot.Column).Item2).ToList();
            }
            else if (startSpot.Column == endSpot.Column)
            {
                path = grid.Where(cell => cell.Column == startSpot.Column && cell.GetRowNumber() >= Compare(startSpot.GetRowNumber(), endSpot.GetRowNumber()).Item1 && cell.GetRowNumber() <= Compare(startSpot.GetRowNumber(), endSpot.GetRowNumber()).Item2).ToList();
            }
            else if (Math.Abs(startSpot.GetRowNumber() - endSpot.GetRowNumber()) == Math.Abs(startSpot.Column - endSpot.Column))
            {
                path = grid.Where(cell => cell.Column >= Compare(startSpot.Column, endSpot.Column).Item1 && cell.Column <= Compare(startSpot.Column, endSpot.Column).Item2 && cell.GetRowNumber() >= Compare(startSpot.GetRowNumber(), endSpot.GetRowNumber()).Item1 && cell.GetRowNumber() <= Compare(startSpot.GetRowNumber(), endSpot.GetRowNumber()).Item2 && Math.Abs(cell.GetRowNumber() - startSpot.GetRowNumber()) == Math.Abs(cell.Column - startSpot.Column)).ToList();
            }

            return path;
        }

        private static double FindDistance(GridSpot spot1, GridSpot spot2)
        {
            return Math.Sqrt(Math.Pow(Math.Abs(spot1.Column - spot2.Column), 2) + Math.Pow(Math.Abs(spot1.GetRowNumber() - spot2.GetRowNumber()), 2));
        }

        private static (int, int) Compare(int a, int b)
        {
            int lowest = a;
            int highest = b;

            if (a > b)
            {
                highest = a;
                lowest = b;
            }

            return (lowest, highest);
        }

        private static Footballer GetFootballer(Player player, string coordinate, List<GridSpot> grid)
        {
            foreach (Footballer footballer in player.Footballers)
            {
                if (footballer.CurrentPosition == GetGridSpot(grid, coordinate))
                {
                    return footballer;
                }
            }

            return player.Footballers[0];
        }

        internal static int DiceRoll()
        {
            Console.WriteLine("Rolling the dice...");
            Random rand = new Random();
            return rand.Next(1, 7);
        }

        internal static bool Run(Player activePlayer, Player opponent, string startCoord, string endCoord, List<GridSpot> grid)
        {
            bool runSuccess = true;

            Console.WriteLine($"Making a move from {startCoord} to {endCoord}...");
            Footballer footballer = GetFootballer(activePlayer, startCoord, grid);
            Footballer oppFootballer = opponent.Footballers[0];

            List<GridSpot> path = GetPath(startCoord, endCoord, grid);
            int oppSpotNumberInPath = 0;

            bool oppFootballerInPath = false;

            for (int i = 0; i < path.Count; i++)
            {
                foreach (Footballer opp in opponent.Footballers)
                {
                    if (opp.CurrentPosition == path[i])
                    {
                        oppFootballerInPath = true;
                        oppFootballer = opp;
                        oppSpotNumberInPath = i;
                        break;
                    }
                }
            }

            if (oppFootballerInPath == true)
            {
                if (footballer.HasBall == true)
                {
                    PlaceFootballer(footballer, path[oppSpotNumberInPath - 1].Row + path[oppSpotNumberInPath - 1].Column.ToString(), grid);
                    footballer.HasBall = false;
                    oppFootballer.HasBall = true;
                    runSuccess = false;
                }
                else if (footballer.HasBall == false && oppFootballer.HasBall == true)
                {
                    PlaceFootballer(footballer, endCoord, grid);
                    footballer.HasBall = true;
                    oppFootballer.HasBall = false;
                }
                else
                {
                    PlaceFootballer(footballer, endCoord, grid);
                }
            }
            else
            {
                PlaceFootballer(footballer, endCoord, grid);
            }

            return runSuccess;
        }

        internal static bool IsAttacking(Player player)
        {
            foreach (Footballer footballer in player.Footballers)
            {
                if (footballer.HasBall == true)
                {
                    return true;
                }
            }

            return false;
        }

        internal static bool Kick(Player activePlayer, Player opponent, string startCoord, List<GridSpot> grid)
        {
            Console.WriteLine("Making a goal kick...");
            Footballer footballer = GetFootballer(activePlayer, startCoord, grid);

            double distance = FindDistance(GetGridSpot(grid, startCoord), GetGridSpot(grid, opponent.GatesCoord));
            if (distance > 6)
            {
                distance = 6;
            }

            Footballer oppGoalkeeper = opponent.Footballers.Where(footballer => footballer.Icon == 'G').ToList()[0];

            double oppGoalkeeperDistance = FindDistance(GetGridSpot(grid, opponent.GatesCoord), oppGoalkeeper.CurrentPosition);
            if (oppGoalkeeperDistance > 4)
            {
                oppGoalkeeperDistance = 4;
            }

            double goalProb = 0.4 * (1 - distance / 6) + 0.3 * (oppGoalkeeperDistance / 4) + 0.2*footballer.ScoringProb;

            System.Threading.Thread.Sleep(3000);

            Random rand = new Random();
            if (rand.Next(1, 101) < goalProb * 100)
            {
                activePlayer.Goals++;
                Console.WriteLine($"Goal! {activePlayer.Name} has scored a goal");
                
                activePlayer.Footballers.Clear();
                opponent.Footballers.Clear();

                foreach (GridSpot spot in grid)
                {
                    spot.Occupied = false;
                }

                if (activePlayer.GatesCoord == "D1")
                {
                    CreateFootballers(activePlayer, opponent, grid, false);
                }
                else
                {
                    CreateFootballers(opponent, activePlayer, grid, true);
                }
            }
            else
            {
                footballer.HasBall = false;
                oppGoalkeeper.HasBall = true;
                PlaceFootballer(oppGoalkeeper, opponent.GatesCoord, grid);
                Console.WriteLine($"No goal.");
            }

            return false;
        }

        internal static bool CoordExists(string coordinate, List<GridSpot> grid)
        {
            if (coordinate.Length == 2 || coordinate.Length == 3)
            {
                foreach (GridSpot spot in grid)
                {
                    if (spot.Row == coordinate[0] && spot.Column == Convert.ToInt32(coordinate[1..]))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        internal static bool IsValidRun(string startCoord, string endCoord, List<GridSpot> grid, int movesLeft)
        {
            GridSpot startSpot = GetGridSpot(grid, startCoord);
            GridSpot endSpot = GetGridSpot(grid, endCoord);

            if (endSpot.Occupied == false && GetPath(startCoord, endCoord, grid).Count-1 <= movesLeft)
            {
                if (startSpot.Column == endSpot.Column)
                {
                    return true;
                }
                else if (startSpot.GetRowNumber() == endSpot.GetRowNumber())
                {
                    return true;
                }
                else if (Math.Abs(startSpot.Column - endSpot.Column) == Math.Abs(startSpot.GetRowNumber() - endSpot.GetRowNumber()))
                {
                    return true;
                }
            }

            return false;
        }

        internal static bool IsValidPass(string startCoord, string endCoord, List<GridSpot> grid, Player activePlayer)
        {
            GridSpot startSpot = GetGridSpot(grid, startCoord);
            GridSpot endSpot = GetGridSpot(grid, endCoord);

            foreach (Footballer footballer in activePlayer.Footballers)
            {
                if (footballer.CurrentPosition == endSpot)
                {
                    if (startSpot.Column == endSpot.Column)
                    {
                        return true;
                    }
                    else if (startSpot.GetRowNumber() == endSpot.GetRowNumber())
                    {
                        return true;
                    }
                    else if (Math.Abs(startSpot.Column - endSpot.Column) == Math.Abs(startSpot.GetRowNumber() - endSpot.GetRowNumber()))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        internal static Footballer GetBallPlayer(Player activePlayer, Player opponent)
        {
            foreach (Footballer footballer in activePlayer.Footballers)
            {
                if (footballer.HasBall == true)
                {
                    return footballer;
                }
            }

            foreach (Footballer oppFootballer in opponent.Footballers)
            {
                if (oppFootballer.HasBall == true)
                {
                    return oppFootballer;
                }
            }

            return activePlayer.Footballers[0];
        }
    }
}
