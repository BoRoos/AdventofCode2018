using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2018
{
    class Day10
    {
        public static void Execute()
        {
            char[] pattern = { '<', '>', ','};
            var data = File.ReadAllLines(@"c:\temp\input10.txt");
            var points = new List<Point>();
            foreach(var row in data)
            {
                var parts = row.Split(pattern, StringSplitOptions.RemoveEmptyEntries);
                var wrk = new Point() {
                    Xpos = int.Parse(parts[1]),
                    Ypos = int.Parse(parts[2]),
                    Xvel = int.Parse(parts[4]),
                    Yvel = int.Parse(parts[5])
                };
                points.Add(wrk);
            }

            var done = false;
            var second = 0;
            var currentdelta = points.Max(y => y.Ypos) - points.Min(y=>y.Ypos);
            while (!done && second < 100000)
            {
                second++;
                foreach (var p in points) p.Move();
                var delta = points.Max(y => y.Ypos) - points.Min(y => y.Ypos);
                if (delta < currentdelta) currentdelta = delta;
                else
                {
                    done = true;
                    foreach (var p in points) p.MoveBack();
                }
            }
            var ymin = points.Min(y => y.Ypos);
            var ymax = points.Max(y => y.Ypos);
            var xmin = points.Min(x => x.Xpos);
            var xmax = points.Max(x => x.Xpos);

            for (var i= ymin; i<= ymax; i++)
            {
                for (var j = xmin; j <= xmax; j++)
                {
                    var found = points.Where(f => f.Xpos == j && f.Ypos == i).Any();
                    if (found) Console.Write("#");
                    else Console.Write(" ");
                }
                Console.WriteLine();
            }

            Console.WriteLine(second-1);
            Console.ReadKey();

        }


    }
    class Point
    {
        public int Xpos;
        public int Ypos;
        public int Xvel;
        public int Yvel;
        public void Move()
        {
            Xpos += Xvel;
            Ypos += Yvel;
        }
        public void MoveBack()
        {
            Xpos -= Xvel;
            Ypos -= Yvel;
        }
    }

}
