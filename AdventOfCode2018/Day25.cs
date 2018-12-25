using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2018
{

    class Day25

    { 
        public void Execute()
        {
            var data = File.ReadAllLines(@"c:\temp\input25.txt");
            var stars = new List<Star>();
            var constellations = new List<Constellation>();
            foreach(var row in data)
            {
                var parts = row.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                stars.Add(new Star
                {
                    X = int.Parse(parts[0]),
                    Y = int.Parse(parts[1]),
                    Z = int.Parse(parts[2]),
                    T = int.Parse(parts[3]),
                });
            };

            //stars = stars.OrderBy(o => CalcDistance(o, new Star { X = 0, Y = 0, Z = 0, T = 0 })).ToList();

            foreach(var s in stars) {

                var clist =new List<Constellation>();
                foreach(var c in constellations)
                {
                   foreach(var cs in c.Stars)
                    {
                        if(CalcDistance(s,cs) <= 3 && !clist.Contains(c))
                        {
                            clist.Add(c);
                        }
                    }
                }
                if (clist.Count() == 0)
                {
                    var wrk = new Constellation();
                    constellations.Add(wrk);
                    clist.Add(wrk);
                }

                var cc = clist.First();
                for(int i =1; i<clist.Count(); i++)
                {

                    cc.Stars = clist[i].Stars.Concat(cc.Stars).ToList(); ;
                    constellations.Remove(clist[i]);
                }

                cc.Stars.Add(s);

            }
            Console.WriteLine(constellations.Count());
            Console.ReadKey();
        }

        int CalcDistance(Star p1, Star p2)
        {
            
                var result = Math.Abs(p1.X - p2.X) + Math.Abs(p1.Y - p2.Y) + Math.Abs(p1.Z - p2.Z) + Math.Abs(p1.T - p2.T);
                return result;
            
        }
    }
    class Star {
        public int X, Y, Z, T;
    }
    class Constellation
    {
        public int Id = 0;
        public List<Star> Stars = new List<Star>();
    }
}

