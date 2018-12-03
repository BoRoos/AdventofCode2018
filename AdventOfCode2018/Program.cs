using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2018
{
    class Program
    {
        static void Main(string[] args)
        {

            //Day1();
            //Day2();
            //Day3();
            Day4();
        }

        static void Day4()
        {
            var data = File.ReadAllLines(@"c:\temp\input.txt");
            var result = 0;


            Console.WriteLine(result);
            Console.ReadKey();
        }
        static void Day3()
        {
            
            var data = File.ReadAllLines(@"c:\temp\input.txt");
            int[,] grid = new int[1000,1000];
            
            foreach(var row in data)
            {
                char[] pattern = { ' ', ':', ',', 'x' };
                var parts = row.Split(pattern);
                var x = int.Parse(parts[2]);
                var y = int.Parse(parts[3]);
                var xmax = x+ int.Parse(parts[5])-1;
                var ymax = y + int.Parse(parts[6]) - 1;
                
                for(var i = x; i<=xmax; i++)

                {
                    for(var j=y; j<=ymax;j++)
                    {
                        grid[i, j]++;
                    }
                }
                               
            }
            var square = 0;
            for( var i= 0; i<1000;i++)
            {
                for(var j=0;j< 1000;j++)
                {
                    if (grid[i, j] > 1) square++;
                }
            }
            Console.WriteLine(square);

            var currentclaim = "";
            foreach (var row in data)
            {
                char[] pattern = { '#','@',' ', ':', ',', 'x' };
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
                        if (grid[i, j] > 1) isfound = false;
                    }
                }
                if (isfound) currentclaim = row;
            }

            Console.WriteLine(currentclaim);
            Console.ReadKey();
        }

    
        static void Day2()
        {
            int result = 0;
            var data = File.ReadAllLines(@"c:\temp\input.txt");
            var times2 = 0;
            var times3 = 0;
            var chars = new Dictionary<char, int>();
            foreach (var row in data)
            {
                chars.Clear();
                foreach (var letter in row.ToCharArray())
                {
                    if (chars.ContainsKey(letter))
                    {
                        chars[letter]++;
                    }
                    else
                    {
                        chars.Add(letter, 1);
                    }
                }

                if (chars.ContainsValue(2))
                {
                    times2++;
                }

                if (chars.ContainsValue(3))
                {
                    times3++;
                }

            }

            result = times2 * times3;
            Console.WriteLine(result);


            for (var i = 0; i < data.Length - 2; i++)
            {
                for (var j = i + 1; j < data.Length - 1; j++)
                {
                    var count = 0;
                    var lettersi = data[i].ToCharArray();
                    var lettersj = data[j].ToCharArray();
                    for (var k = 0; k < lettersi.Length - 1; k++)
                    {
                        if (lettersi[k] != lettersj[k])
                        {
                            count++;
                        }
                    }
                    if (count == 1)
                    {
                        Console.WriteLine(data[i]);
                        Console.WriteLine(data[j]);
                       
                    }
                }
            }

            Console.ReadKey();
        }
        static void Day1()
        {

            var data = File.ReadAllLines(@"c:\temp\input.txt");

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
