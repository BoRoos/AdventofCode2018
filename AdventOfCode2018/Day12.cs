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
            var initialstate = data[0].Split(' ')[2];
            var pots = new int[2000];
            var origo = 100;

            
            for (int i = 0; i < initialstate.Length; i++)
            {
                if (initialstate[i] == '#') pots[i+origo] = 1;
            }

            var rules = new Dictionary<int, int>();
            foreach (var row in data)
            {
                if (row.Contains("="))
                {
                    var pattern = Convert.ToInt32(row.Split()[0].Replace(".","0").Replace("#","1"),2);
                    var value = (row.Split()[2] == "#" ? 1 : 0);
                    rules.Add(pattern, value);
                }
            }

            var generations = new Dictionary<string, PotGeneration>();
            var pg = new PotGeneration {
                gen = 0,
                value = CalcResult(pots, origo),
                key = GetKey(pots, origo),
                pots = pots
            };

            generations.Add(pg.key, pg);
            var nextpots = pots;
            var currentgen = 0;
            for(  currentgen=1; currentgen<=20; currentgen++)
            {
                nextpots = GetNextGen(nextpots, origo, rules);
                pg = new PotGeneration
                {
                    gen = currentgen,
                    value = CalcResult(nextpots, origo),
                    key = GetKey(nextpots, origo),
                    pots = nextpots
                };
                generations.Add(GetKey(nextpots, origo), pg);
            }
            var result = CalcResult(nextpots,origo);
            
            Console.WriteLine(result);
                        
            nextpots = GetNextGen(nextpots, origo, rules);
            var key = GetKey(nextpots, origo);
            
            while (!generations.ContainsKey(key))
            {
                pg = new PotGeneration
                {
                    gen = ++currentgen,
                    value = CalcResult(nextpots, origo),
                    key = GetKey(nextpots, origo),
                    pots = nextpots
                };
                generations.Add(GetKey(nextpots, origo), pg);

                nextpots = GetNextGen(nextpots, origo, rules);
                key = GetKey(nextpots, origo);
                Console.WriteLine(CalcResult(nextpots, origo));
            }
            var calc = 50000000000 % generations.Count();
            foreach(var w in generations.Values)
            {
                Console.WriteLine(w.gen + ":" + w.value);
            }


            Console.WriteLine(CalcResult(nextpots, origo));
                                         
            Console.ReadKey();

        }
        static int[] GetNextGen(int[] pots, int origo, Dictionary<int,int> rules  )
        {
            var NextGen = new int[2000];
            for (var i = -98 + origo; i < 1000 + origo; i++)
            {
                var key = pots[i - 2] * 16 + pots[ i - 1] * 8 + pots[ i] * 4 + pots[ i + 1] * 2 + pots[ i + 2] * 1;
                var value = 0;
                if (rules.ContainsKey(key)) value = rules[key];
                NextGen[i] = value;
                
            }
            return NextGen;
        }
        static int CalcResult(int[] pots, int origo)
        {
            var result = 0;
            for (var i = -98 + origo; i < 1000 + origo; i++)
            {
                result += pots[i] * (i - origo);
            }
            return result;
        }
        static Boolean GenIsEqual(int[] curgen, int[] nextgen, int origo)
        {
            
            for (var i = -98 + origo; i < 1000 + origo; i++)
            {
                if (curgen[i] != nextgen[i]) return false;
            }
            return true;
        }
        static string GetKey(int[] pots, int origo)
        {
            var result = new StringBuilder(2000);
            for (var i = -98 + origo; i < 1000 + origo; i++)
            {
                result.Append(pots[i] == 1 ? "#" : ".");
            }
            return result.ToString();
        }
    }
    class PotGeneration
    {
        public int gen;
        public int value;
        public string key;
        public int[] pots;
    }

}
