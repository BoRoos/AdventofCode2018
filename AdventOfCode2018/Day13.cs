using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2018
{
    class Day13
    {
        public static void Execute()
        {
            var data = File.ReadAllLines(@"c:\temp\input13.txt");
            var railway = new char[150, 150];
            var carts = new List<Cart>();
            var cartfilter = "<>^v";
            int x, y;
            y = 0;
            foreach (var row in data)
            {
                x = 0;
                foreach (var part in row.ToCharArray())
                {
                    var track = part;
                    if (cartfilter.Contains(part))
                    {
                        var c = new Cart
                        {
                            id = carts.Count(),
                            x = x,
                            y = y,
                            direction = part
                        };
                        carts.Add(c);
                        if (track == '<' || track == '>') track = '-';
                        else track = '|';
                    }

                    railway[x, y] = track;
                    x++;
                }
                y++;
            }
            int tick = 0;
            while (carts.Where(f=>!f.Crashed).Count()>1)
            {
                //for (y = 0; y < 10; y++)
                //{
                //    for (x = 0; x < 15; x++)
                //    {
                //        if (carts.Where(f => f.x == x && f.y == y && !f.Crashed).Any())
                //            Console.Write(carts.Where(f => f.x == x && f.y == y).First().direction);
                //        else
                //            Console.Write(railway[x, y]);
                //    }
                //    Console.WriteLine();
                //}

                foreach (var c in carts.Where(f=>!f.Crashed).OrderBy(o => o.y).ThenBy(o => o.x))
                {
                    if (!c.Crashed) c.Move(railway);
                    //Console.WriteLine($"Cart: {c.id} x:{c.x} y:{c.y} dir:{c.direction}");
                                        
                    if(carts.Where(f=>f.x==c.x && f.y==c.y && f.id != c.id && !f.Crashed).Any())
                    {
                        carts.Where(f => f.x == c.x && f.y == c.y && f.id != c.id).First().Crashed = true;
                        c.Crashed = true;
                        Console.WriteLine($"{tick} Crasched Cart: {c.id} x:{c.x} y:{c.y} dir:{c.direction}");
                    }
                }
                
                tick++;
            }
            var l = carts.Where(f => !f.Crashed).First();
            
            Console.WriteLine($"Last Cart: {l.id} x:{l.x} y:{l.y} dir:{l.direction}");
            Console.ReadKey();

        }
    }
        class Cart {
            public int id = 0;
            public int x;
            public int y;
            public char direction;
            public char turn = 'L';
            public bool Crashed = false;
        public void Move(char[,] railway)
        {
            var nextx =x;
            var nexty=y;
            var nextdirection = direction;
            var nextturn = turn;
            if (direction == '<') nextx--;
            if (direction == '>') nextx++;
            if (direction == 'v') nexty++;
            if (direction == '^') nexty--;
            var nextpos = railway[nextx, nexty];
            switch (nextpos)
            {
                case '/':
                    if (direction == '<') nextdirection = 'v';
                    if (direction == '>') nextdirection = '^';
                    if (direction == 'v') nextdirection = '<';
                    if (direction == '^') nextdirection = '>';
                    break;
                case '\\':
                    if (direction == '<') nextdirection = '^';
                    if (direction == '>') nextdirection = 'v';
                    if (direction == 'v') nextdirection = '>';
                    if (direction == '^') nextdirection = '<';
                    break;
                case '+':
                    if (turn == 'L') nextturn = 'S';
                    if (turn == 'S') nextturn = 'R';
                    if (turn == 'R') nextturn = 'L';

                    if (direction == '<' && turn == 'L') nextdirection = 'v';
                    if (direction == '<' && turn == 'R') nextdirection = '^';
                    if (direction == '>' && turn == 'L') nextdirection = '^';
                    if (direction == '>' && turn == 'R') nextdirection = 'v';

                    if (direction == 'v' && turn == 'L') nextdirection = '>';
                    if (direction == 'v' && turn == 'R') nextdirection = '<';
                    if (direction == '^' && turn == 'L') nextdirection = '<';
                    if (direction == '^' && turn == 'R') nextdirection = '>';
                    break;
                default:
                    break;
            }

            x = nextx;
            y = nexty;
            turn = nextturn;
            direction = nextdirection;
        }

    }

}
