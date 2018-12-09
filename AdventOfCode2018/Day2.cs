using System;
using System.Collections.Generic;
using System.IO;
namespace AdventOfCode2018
{
    class Day2
    {
        public static void Execute()
        {
            int result = 0;
            var data = File.ReadAllLines(@"c:\temp\input2.txt");
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

    }
}
