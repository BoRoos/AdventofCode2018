using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2018
{
    class Day12
    {
        public static void Execute()
        {
            var data = File.ReadAllLines(@"c:\temp\input12.txt");
            string initialstate = data[0].Split(' ')[2];
            int origo = 0;
            Dictionary<string, char> rules = new Dictionary<string, char>();
            foreach (var row in data)
            {
                if (row.Contains("="))
                {
                    string pattern = row.Split()[0];
                    var value = row.Split()[2][0];
                    if (value=='#') rules.Add(pattern, value);
                }
            }
            string currentpots = initialstate;
            int[] scores = new int[2000];
            for (var gen = 0; gen < 1000; gen++)
            {
                var score = CalcResult(currentpots, origo);
                //if (gen > 0) Console.WriteLine(score + " " + (score - scores[gen - 1]));
                scores[gen] = score;
                currentpots = "...." + currentpots + "....";
                origo += 4;
                currentpots = GetNextGen(currentpots, rules);
            }
            Console.WriteLine(scores[20]);
            long finalscore = (50000000000 - 999) * (scores[999] - scores[998]) + scores[999];
            Console.WriteLine(finalscore);

            Console.ReadKey();

        }
        static string GetNextGen(string pots, Dictionary<string, char> rules)
        {
            StringBuilder results = new StringBuilder(pots.Length);
            results.Append("..");

            for (int i = 2; i < pots.Length - 2; i++)
            {
                string key = new string(new char[] { pots[i - 2], pots[i - 1], pots[i], pots[i + 1], pots[i + 2] });

                if (rules.ContainsKey(key)) results.Append(rules[key]);
                else results.Append(".");

            }
            results.Append("..");
            return results.ToString();
        }
        static int CalcResult(string pots, int origo)
        {
            var result = 0;

            for (var i = 0; i < pots.Length; i++)
            {
                result += pots[i] == '#' ? i - origo : 0;
            }
            return result;
        }
    }    
}
