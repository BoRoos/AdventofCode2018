using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018
{
    class Day7
    {
        public static void execute()
        {
            var data = File.ReadAllLines(@"c:\temp\inputtest.txt");
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
                else first = Steps[firstname];
                if (!Steps.ContainsKey(followname))
                {
                    Steps.Add(followname, follow);
                }
                else
                    follow = Steps[followname];
                first.Followlist.Add(followname,follow);
                follow.Prevlist.Add(firstname,first);

            }
            var Headoflist = from s in Steps where s.Value.Prevlist.Count() == 0 select s;
            var result = GetPath(Headoflist.First().Value);

            Console.WriteLine(result);
            Console.ReadKey();

        }
        static string GetPath(Step HeadofList)
        {
            var result = HeadofList.Name;
            foreach( var nexthead in HeadofList.Followlist)
            {
                result += GetPath(nexthead.Value);
            }
            return result;
        }

    }
    class Step {
        public string Name;
        public SortedList<string, Step> Prevlist = new SortedList<string, Step>();
        public SortedList<string, Step> Followlist = new SortedList<string, Step>();
        public Step(string name)
        {
            Name = name;
        
        }
}
}
