using System;
using System.Collections.Generic;
using System.IO;
namespace AdventOfCode2018
{
    class Day4
    {
        public static void Execute()
        {
            var data = File.ReadAllLines(@"c:\temp\input4.txt");


            var ordlist = new SortedList<DateTime, string>();
            foreach (var row in data)
            {
                ordlist.Add(DateTime.Parse(row.Substring(1, 16)), row.Substring(19));
            }


            var guards = new Dictionary<int, Guard>();
            var current = new Guard();
            foreach (var row in ordlist)
            {
                var minute = row.Key.Minute;
                var segment = row.Value;
                if (segment.Contains("begin"))
                {

                    var key = int.Parse(row.Value.Split()[1].Substring(1));
                    if (current.Id > 0)
                    {
                        current.WakeUp(minute);
                    }
                    if (guards.ContainsKey(key))
                    {
                        current = guards[key];
                    }
                    else
                    {
                        current = new Guard();
                        current.Id = key;

                        guards.Add(current.Id, current);
                    }
                }

                if (segment.Contains("asleep"))
                {
                    current.Sleep(minute);
                }
                if (segment.Contains("wakes"))
                {
                    current.WakeUp(minute);
                }

            }

            current = new Guard();
            foreach (var g in guards.Values)
            {
                if (g.TotalAsleep() > current.TotalAsleep())
                {
                    current = g;
                }
            }


            Console.WriteLine(current.Id * current.MostAsleep());
            current = new Guard();
            foreach (var g in guards.Values)
            {
                if (g.MostAsleepAmount() > current.MostAsleepAmount())
                {
                    current = g;
                }
            }
            Console.WriteLine(current.Id * current.MostAsleep());


            Console.ReadKey();
        }

    }
    public class Guard
    {
        public int Id;
        public Dictionary<int, int> sleeps;
        public int asleep;
        private bool sleeping = false;
        public Guard()
        {
            sleeps = new Dictionary<int, int>();
            for (var i = 0; i < 60; i++)
            {
                sleeps.Add(i, 0);
            }
        }
        public void Sleep(int minute)
        {
            sleeping = true;
            asleep = minute;
        }
        public void WakeUp(int minute)
        {
            if (sleeping)
            {
                for (var i = asleep; i < minute; i++)
                {
                    sleeps[i]++;
                }
                sleeping = false;
            }
        }
        public int TotalAsleep()
        {
            var result = 0;
            for (var i = 0; i < 60; i++)
            {
                result += sleeps[i];
            }
            return result;
        }
        public int MostAsleep()
        {
            var current = 0;
            var minute = 0;
            for (var i = 0; i < 60; i++)
            {
                if (sleeps[i] > current)
                {
                    current = sleeps[i];
                    minute = i;
                }
            }
            return minute;
        }
        public int MostAsleepAmount()
        {
            var current = 0;
            for (var i = 0; i < 60; i++)
            {
                if (sleeps[i] > current)
                {
                    current = sleeps[i];

                }
            }
            return current;
        }
    }
}
