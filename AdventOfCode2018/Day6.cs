using System.Collections.Generic;
using System.IO;
using System;

namespace AdventOfCode2018
{
    class Day6
    {
        public static void Execute()
        {
            var data = File.ReadAllLines(@"c:\temp\input6.txt");
            var coordlist = new List<Coord>();
            int xmin=1000, xmax=0, ymin=1000, ymax=0;
            foreach (var row in data)
            {
                
                if (row.Length > 1)
                {
                    var parts = row.Split(',');
                    var c = new Coord(int.Parse(parts[0]), int.Parse(parts[1]));
                    coordlist.Add(c);
                    if (c.X < xmin) xmin = c.X;
                    if (c.X > xmax) xmax = c.X;
                    if (c.Y < ymin) ymin = c.Y;
                    if (c.Y > ymax) ymax = c.Y;
                }

            }


            var SizeRegion = 0;
            for (var i = xmin; i < xmax; i++)
            {
                for (var j = ymin; j < ymax; j++)
                {
                    var currentmhdist = 10000;
                    var duplicate = false;
                    var cur = new Coord(0, 0);
                    var totaldistance = 0;
                
                    foreach (var c in coordlist)
                    {

                        var mhdist = CalcManhattan(i, j, c.X, c.Y);
                        if(mhdist == currentmhdist) duplicate = true;
                        if (mhdist < currentmhdist)
                        {
                            currentmhdist = mhdist;
                            cur = c;
                            duplicate = false;
                        }
                        totaldistance += mhdist;

                    }
                    if (totaldistance < 10000) SizeRegion++;

                    if (!duplicate && xmin < cur.X && xmax > cur.X && ymin < cur.Y && ymax > cur.Y)
                    {
                        cur.ClosestCoords++;
                    }
                }
            }
            //foreach (var c in coordlist)
            //{
            //    Console.WriteLine(string.Format("x:{0} y:{1} ClosestNo: {2}", c.X, c.Y, c.ClosestCoords));
            //}

            Console.WriteLine(SizeRegion);
            Console.ReadKey();

        }
        static int CalcManhattan(int x1, int y1, int x2, int y2)
        {
            var result = Math.Abs(x2 - x1) + Math.Abs(y2 - y1);
            return result;
        }

    }
    class Coord
    {
        public int X;
        public int Y;
        public int ClosestCoords;
        public Coord(int newx, int newy)
        {
            X = newx;
            Y = newy;
            
        }
    }
}
