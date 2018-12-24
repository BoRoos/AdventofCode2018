using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2018
{
    class Day15
    {
        public static void Execute()
        {
            var data = File.ReadAllLines(@"c:\temp\input15.txt");
            char[,] map = new char[32, 32];
            var players = new List<Player>();
            int x, y;
            y = 0;
            foreach(var row in data)
            {
                x = 0;
                foreach(var c in row.ToCharArray())
                {
                    map[x, y] = c;
                    if (c=='E' || c=='G')
                    {
                        var p = new Player { X = x, Y = y, UnitType = c, AttackPower=3 };
                        players.Add(p);
                        map[x, y] = '.';
                    }
                    x++;
                }
                y++;
            }

            var timer = new Stopwatch();
            timer.Start();
            int result = PlayGame(map, players, false);
            Console.WriteLine(timer.ElapsedMilliseconds + " "+ result);
            timer.Restart();

            for (int elfAttackPower = 3; elfAttackPower <= 20; elfAttackPower++)
            {
                map = new char[32, 32];
                players = new List<Player>();
                y = 0;
                foreach (var row in data)
                {
                    x = 0;
                    foreach (var c in row.ToCharArray())
                    {
                        map[x, y] = c;
                        if (c == 'E' || c == 'G')
                        {
                            var p = new Player { X = x, Y = y, UnitType = c, AttackPower = 3 };
                            if (p.UnitType == 'E') p.AttackPower = elfAttackPower;
                            players.Add(p);
                            map[x, y] = '.';
                        }
                        x++;
                    }
                    y++;
                }

                result = PlayGame(map, players, true);

                if (result > 0)
                {
                    //Console.WriteLine(result);
                    break;
                }
            }
            Console.WriteLine(timer.ElapsedMilliseconds + " " + result);

            Console.ReadKey();
        }
        static int PlayGame( char[,] map, List<Player> players, bool failonElfDeath)
        {
            for (var round=0; ;round++)
            {
                //Console.WriteLine($"{round}  {players.Sum(s => s.Health)}");
                players = players.OrderBy(o => o.Y).ThenBy(o => o.X).ToList();
                for (int i = 0; i < players.Count(); i++)
                {
                    Player p = players[i];
                    List<Player> targets = players.Where(t => t.UnitType != p.UnitType).ToList();
                    if (targets.Count == 0)
                    {

                        return round * players.Sum(s => s.Health);
                    }

                    //if no adjacent move
                    if (!targets.Any(t => IsAdjacent(t, p))) 
                        TryToMove(p, targets, map,players);

                    //Attack

                    Player bestAdjacent =
                        targets
                        .Where(t => IsAdjacent(p, t))
                        .OrderBy(t => t.Health)
                        .ThenBy(t => t.Y)
                        .ThenBy(t => t.X)
                        .FirstOrDefault();

                    if (bestAdjacent == null)
                        continue;

                    bestAdjacent.Health -= p.AttackPower;
                    if (bestAdjacent.Health > 0)
                        continue;

                    if (failonElfDeath && bestAdjacent.UnitType=='E')
                    {
                            return 0;
                    }
                        

                    int index = players.IndexOf(bestAdjacent);
                    players.RemoveAt(index);
                    if (index < i)
                        i--;

                }
              
            }

        }
        static void TryToMove(Player p, List<Player> targets, char[,] map,List<Player> players)
        {
            (int dx, int dy)[] NearbySquares = { (0, -1), (-1, 0), (1, 0), (0, 1) };
            HashSet<(int x,int y)> inRange = new HashSet<(int x, int y)>();
            foreach (var t in targets)
            {

                foreach ((int dx, int dy) in NearbySquares)
                {
                    (int nx, int ny) = (t.X + dx, t.Y + dy);
                    if (IsOpenSquare(nx, ny,map,players))
                        inRange.Add((nx, ny));
                }
            }

            Queue<(int x, int y)> SquaresToCheck = new Queue<(int x, int y)>();
            Dictionary<(int x,int y), (int px, int py)> CheckedSquares = new Dictionary<(int x, int y), (int px, int py)>();
            SquaresToCheck.Enqueue((p.X, p.Y));
            CheckedSquares.Add((p.X, p.Y), (-1, -1));
            while (SquaresToCheck.Count() >0)
            {
                (int x, int y) prevpos = SquaresToCheck.Dequeue();
                foreach ((int dx, int dy) in NearbySquares)
                {
                    (int x, int y) nsq = (prevpos.x + dx, prevpos.y + dy);
                                                           
                    if (!CheckedSquares.ContainsKey(nsq) && IsOpenSquare(nsq.x,nsq.y, map, players))
                    {
                        SquaresToCheck.Enqueue(nsq);
                        CheckedSquares.Add(nsq, prevpos);
                    }
                                                               
                }
            }
            List<(int tx, int ty, List<(int x, int y)> path)> Validpaths =
            inRange
            .Select(t => (t.x, t.y, path: getPath(p,t.x, t.y, CheckedSquares)))
            .Where(t => t.path != null)
            .OrderBy(t => t.path.Count)
            .ThenBy(t => t.y)
            .ThenBy(t => t.x)
            .ToList();

            List<(int x, int y)> bestPath = Validpaths.FirstOrDefault().path;
            if (bestPath != null)
                (p.X, p.Y) = bestPath[0];
        }
        static bool IsOpenSquare(int x, int y, char[,] map, List<Player> players) {
           return map[x, y] == '.' && players.All(p => p.X != x || p.Y != y);
        }
        static bool IsAdjacent(Player p1, Player p2) => Math.Abs(p1.X - p2.X) + Math.Abs(p1.Y - p2.Y) == 1;

        static List<(int x, int y)> getPath(Player p, int destX, int destY, Dictionary<(int x, int y), (int px, int py)> CheckedSquares)
        {
            if (!CheckedSquares.ContainsKey((destX, destY)))
                return null;
            List<(int x, int y)> path = new List<(int x, int y)>();
            (int x, int y) = (destX, destY);
            while (x != p.X || y != p.Y)
            {
                path.Add((x, y));
                (x, y) = CheckedSquares[(x, y)];
            }

            path.Reverse();
            return path;
        }
    }
}

public class Player
{
    public int X,Y;
    public char UnitType;
    public int Health=200;
    public int AttackPower = 0;

}

