using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace AdventOfCode2018
{
    class Day1
    {
        public static void Execute()
        {
            var data = File.ReadAllLines(@"c:\temp\input1.txt");

            var freq = 0;
            foreach (var i in data)
            {
                freq += int.Parse(i);
            }
            Console.WriteLine(freq);

            var dups = new List<int>()
            {
                0
            };
            freq = 0;
            var currentpos = 0;
            var found = false;
            //data = @"+3, +3, +4, -2, -4".Split(',');
            //data = @"-6, +3, +8, +5, -6".Split(',');
            //data = @"+7, +7, -2, -7, -4".Split(',');

            while (!found)
            {
                freq += int.Parse(data[currentpos++]);
                if (currentpos == data.Length)
                {
                    currentpos = 0;
                }

                if (dups.Contains(freq))
                {
                    found = true;
                }
                else
                {
                    dups.Add(freq);
                }
            }
            Console.WriteLine(freq);
            Console.ReadKey();
        }

    }
}
