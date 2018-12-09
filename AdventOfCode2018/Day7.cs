using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2018
{
    class Day7
    {
        public static void Execute()
        {
            var data = File.ReadAllLines(@"c:\temp\input7.txt");
            var Steps = new Dictionary<string, Step>();
            foreach (var row in data)
            {
                var parts = row.Split(' ');
                var firstname = parts[1];
                var followname = parts[7];
                var first = new Step(firstname);
                var follow = new Step(followname);
                if (!Steps.ContainsKey(firstname))
                {
                    Steps.Add(firstname, first);
                }
                else
                {
                    first = Steps[firstname];
                }

                if (!Steps.ContainsKey(followname))
                {
                    Steps.Add(followname, follow);
                }
                else
                {
                    follow = Steps[followname];
                }

                first.Followlist.Add(followname, follow);
                follow.Prevlist.Add(firstname, first);

            }
            var Headoflist = (from s in Steps.Values where s.Prevlist.Count() == 0 select s).ToList();
            var path = "";
            while (Headoflist.Count() > 0)
            {
                var head = Headoflist.OrderBy(o => o.Name).First();
                path += head.Name;
                Headoflist.Remove(head);
                foreach (var f in head.Followlist)
                {
                    if (!f.Value.Prevlist.Where(a => !path.Contains(a.Key)).Any() && !path.Contains(f.Key))
                    {
                        Headoflist.Insert(0, f.Value);
                    }
                }


            }

            Console.WriteLine(path);
            // star 2
            Headoflist = (from s in Steps.Values where s.Prevlist.Count() == 0 select s).ToList();
            path = "";
            var second = 0;
            while (Headoflist.Count() > 0)
            {
                //Console.Write("Second: " + second);
                var removing = Headoflist.Where(f => f.steptime == 0).ToList();
                foreach (var head in removing)
                {
                    //Console.WriteLine("Removed: " + head.Name);
                    path += head.Name;
                    Headoflist.Remove(head);
                    foreach (var f in head.Followlist)
                    {
                        if (!f.Value.Prevlist.Where(a => !path.Contains(a.Key)).Any() && !path.Contains(f.Key))
                        {
                            Headoflist.Insert(0, f.Value);
                        }
                    }
                }

                foreach (var worker in Headoflist.OrderBy(o => o.working ? 0 : 1).ThenBy(o => o.Name).Take(5).OrderBy(o=>o.Name))
                {
                    //Console.Write("\t" + worker.Name);
                    worker.working = true;
                    worker.steptime--;
                }

                //Console.WriteLine("\t" + path);
                second++;
            }
            Console.WriteLine(path);
            Console.ReadKey();

        }
        static List<string> GetPath(Step HeadofList, List<string> done)

        {
            var result = done;
            result.Add(HeadofList.Name);
            foreach (var nexthead in HeadofList.Followlist)
            {
                var okToadd = true;
                foreach (var prevName in nexthead.Value.Prevlist.Keys)
                {
                    if (!done.Contains(prevName))
                    {
                        okToadd = false;
                    }
                }
                if (okToadd)
                {
                    var wrk = GetPath(nexthead.Value, result);
                    foreach (var step in wrk)
                    {
                        if (!result.Contains(step))
                        {
                            result.Add(step);
                        }
                    }
                }

            }
           

            return result;
        }

    }
    class Step
    {
        public string Name;
        public int steptime;
        public Boolean working = false;
        public SortedList<string, Step> Prevlist = new SortedList<string, Step>();
        public SortedList<string, Step> Followlist = new SortedList<string, Step>();
        public Step(string name)
        {
            Name = name;
            steptime = name[0] - 65 + 1 + 60;
        }
    }
}
