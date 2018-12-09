using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2018
{
    class Day3
    {
        public static void Execute()
        {
            var data = File.ReadAllLines(@"c:\temp\input3.txt");
            int[,] grid = new int[1000, 1000];

            foreach (var row in data)
            {
                char[] pattern = { ' ', ':', ',', 'x' };
                var parts = row.Split(pattern);
                var x = int.Parse(parts[2]);
                var y = int.Parse(parts[3]);
                var xmax = x + int.Parse(parts[5]) - 1;
                var ymax = y + int.Parse(parts[6]) - 1;

                for (var i = x; i <= xmax; i++)

                {
                    for (var j = y; j <= ymax; j++)
                    {
                        grid[i, j]++;
                    }
                }

            }
            var square = 0;
            for (var i = 0; i < 1000; i++)
            {
                for (var j = 0; j < 1000; j++)
                {
                    if (grid[i, j] > 1)
                    {
                        square++;
                    }
                }
            }
            Console.WriteLine(square);

            var currentclaim = "";
            foreach (var row in data)
            {
                char[] pattern = { '#', '@', ' ', ':', ',', 'x' };
                var parts = row.Split(pattern, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
                var x = parts[1];
                var y = parts[2];
                var xmax = x + parts[3] - 1;
                var ymax = y + parts[4] - 1;
                var isfound = true;

                for (var i = x; i <= xmax; i++)

                {
                    for (var j = y; j <= ymax; j++)
                    {
                        if (grid[i, j] > 1)
                        {
                            isfound = false;
                        }
                    }
                }
                if (isfound)
                {
                    currentclaim = row;
                }
            }

            Console.WriteLine(currentclaim);
            Console.ReadKey();
        }

    }


}
